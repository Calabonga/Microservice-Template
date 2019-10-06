using Newtonsoft.Json;

namespace $safeprojectname$.Infrastructure.ViewModels.AccountViewModels
{
    /// <summary>
    /// Login model
    /// If both Password and Pin specified, password used only
    /// 
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email to login with. Booking is expected with this email address.
        /// </summary>
        [JsonRequired]
        [JsonProperty("login")]
        public string Email { get; set; }

        /// <summary>
        /// Password to login with (optional)
        /// </summary>
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        /// Pin to login with (optional)
        /// </summary>
        [JsonProperty("pin", NullValueHandling = NullValueHandling.Ignore)]
        public string Pin { get; set; }

    }
}
