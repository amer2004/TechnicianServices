using Microsoft.Extensions.Logging;

namespace TechnicalServices
{
    public static class MauiProgram
    { 
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiMaps();
            
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
            builder.Services.AddSingleton<IMap>(Map.Default);
            builder.Services.AddSingleton<OrderService>();
            builder.Services.AddSingleton<PaymentService>();
            builder.Services.AddSingleton<MainServiceService>();
            builder.Services.AddSingleton<TechnicianService>();
            builder.Services.AddSingleton<NotificationService>();
            builder.Services.AddSingleton<FeedBackService>();
            builder.Services.AddSingleton<RatingService>();
            builder.Services.AddSingleton<BaseViewModel>();
            builder.Services.AddSingleton<UserService>();

            builder.Services.AddSingleton<LogInView>();
            builder.Services.AddSingleton<LogInViewModel>();

            builder.Services.AddSingleton<RegistrationView>();
            builder.Services.AddSingleton<RegistrationViewModel>();

            builder.Services.AddSingleton<EmailVerificationView>();
            builder.Services.AddSingleton<EmailVerificationViewModel>();

            builder.Services.AddSingleton<ForgotenPasswordView>();
            builder.Services.AddSingleton<ForgotenPasswordViewModel>();

            builder.Services.AddSingleton<ForgotenPasswordEmailVerficationView>();
            builder.Services.AddSingleton<ForgotenPasswordEmailVerficationViewModel>();

            builder.Services.AddSingleton<ForgotenPasswordResetView>();
            builder.Services.AddSingleton<ForgotenPasswordResetViewModel>();

            builder.Services.AddSingleton<PasswordView>();
            builder.Services.AddSingleton<PasswordViewModel>();

            builder.Services.AddSingleton<LocationView>();
            builder.Services.AddSingleton<LocationViewModel>();

            builder.Services.AddSingleton<UserHomePageView>();
            builder.Services.AddSingleton<TechnicaianHomePageView>();
            builder.Services.AddSingleton<HomePageViewModel>();

            builder.Services.AddSingleton<NotificationDetailsView>();
            builder.Services.AddSingleton<NotificationDetailsViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
