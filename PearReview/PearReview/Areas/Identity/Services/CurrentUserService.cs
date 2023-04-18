using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Identity.Data;
using PearReview.Data;
using System.Security.Claims;

namespace PearReview.Areas.Identity.Services
{
    public class CurrentUserService
    {
        private AuthenticationStateProvider _authProv;
        private UserManager<AppUser> _userManager;

        public CurrentUserService(AuthenticationStateProvider authProv, UserManager<AppUser> userManager)
        {
            if (authProv == null || userManager == null)
            {
                throw new NullReferenceException();
            }

            _authProv = authProv;
            _userManager = userManager;
        }

        public async Task<AuthenticationState> GetAuthState()
        {
            return await _authProv.GetAuthenticationStateAsync();
        }

        public async Task<AppUser?> GetCurrentUser()
        {
            AuthenticationState authState = await GetAuthState();

            if (authState.User.Identity == null || !authState.User.Identity.IsAuthenticated)
            {
                // Not logged in
                return null;
            }

            string? userId = authState.User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser? currentUser = null;

            if (userId != null)
            {
                currentUser = await _userManager.FindByIdAsync(userId);
            }

            return currentUser;
        }
    }
}
