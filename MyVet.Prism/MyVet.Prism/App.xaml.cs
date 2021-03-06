﻿using Prism;
using Prism.Ioc;
using MyVet.Prism.ViewModels;
using MyVet.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyVet.Common.Services;
using Newtonsoft.Json;
using MyVet.Common.Models;
using MyVet.Common.Helpers;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyVet.Prism
{
    public partial class App
    {
        
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            //Agregamos licencia de https://www.syncfusion.com/account/manage-trials/downloads que expira en un mes 27/03/2020 al 27/04/2020
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjI4NDAxQDMxMzcyZTM0MmUzMFBxU0lZZUFzOFNqdkhBbGZKMDJLaHBxTVBKV0RsZ0Z3YWp2QlgrMVhtVlU9");
            InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/VeterinaryMasterDetailPage/NavigationPage/PetsPage");
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PetsPage, PetsPageViewModel>();
            containerRegistry.RegisterForNavigation<PetPage, PetPageViewModel>();
            containerRegistry.RegisterForNavigation<HistoriesPage, HistoriesPageViewModel>();
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<PetTabbedPage, PetTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<VeterinaryMasterDetailPage, VeterinaryMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<AgendaPage, AgendaPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<EditPet, EditPetViewModel>();
        }
    }
}
