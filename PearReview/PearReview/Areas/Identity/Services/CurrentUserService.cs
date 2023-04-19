using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PearReview.Areas.Identity.Data;

namespace PearReview.Areas.Identity.Services
{
    public class CurrentUserService
    {
        private AuthenticationStateProvider authProv;
        private UserManager<AppUser> userMngr;

        private AppUser? currentUser;

        public CurrentUserService(AuthenticationStateProvider authProvider, UserManager<AppUser> userManager)
        {
            if (authProvider == null || userManager == null)
            {
                throw new NullReferenceException();
            }

            authProv = authProvider;
            userMngr = userManager;
        }

        public async Task<AuthenticationState> GetAuthState()
        {
            return await authProv.GetAuthenticationStateAsync();
        }

        public async Task<AppUser?> GetCurrentUser()
        {
            AuthenticationState authState = await GetAuthState();

            if (authState.User.Identity == null || !authState.User.Identity.IsAuthenticated)
            {
                // Not logged in
                ClearCurrentUser();
                return null;
            }

            if (currentUser != null)
            {
                // Cache the user so multiple successive requests are more performant
                return currentUser;
            }

            currentUser = await userMngr.GetUserAsync(authState.User);

            return currentUser;
        }

        public void ClearCurrentUser()
        {
            currentUser = null;
        }
    }
}
