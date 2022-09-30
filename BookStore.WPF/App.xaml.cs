using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using BookStore.Domain.Commands;
using BookStore.Domain.Queries;
using BookStore.EntityFramework;
using BookStore.EntityFramework.Commands;
using BookStore.EntityFramework.Queries;
using BookStore.WPF.Stores;
using BookStore.WPF.ViewModels;
using BookStore.WPF.HostBuilders;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGetAllBookStoreQuery, GetAllBookStoreQuery>();
                    services.AddSingleton<ICreateBookCommand, CreateBookCommand>();
                    services.AddSingleton<IUpdateBookCommand, UpdateBookCommand>();
                    services.AddSingleton<IDeleteBookCommand, DeleteBookCommand>();

                    services.AddSingleton<ModalNavigationStorage>();
                    services.AddSingleton<BookStorage>();
                    services.AddSingleton<SelectedBookStorage>();

                    services.AddTransient<BookStoreViewModel>(CreateBookStoreViewModel);
                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();


            BookStoreDbContextFactory BookStoreDbContextFactory = 
                _host.Services.GetRequiredService<BookStoreDbContextFactory>();
            using(BookStoreDbContext context = BookStoreDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

        private BookStoreViewModel CreateBookStoreViewModel(IServiceProvider services)
        {
            return BookStoreViewModel.LoadViewModel(
                services.GetRequiredService<BookStorage>(),
                services.GetRequiredService<SelectedBookStorage>(),
                services.GetRequiredService<ModalNavigationStorage>());
        }
    }
}
