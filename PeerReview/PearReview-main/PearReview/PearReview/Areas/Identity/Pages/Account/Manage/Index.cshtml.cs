// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PearReview.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PearReview.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AuthenticationStateProvider _authProv;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            AuthenticationStateProvider authProv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authProv = authProv;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [EnumDataType(typeof(UserRole))]
            [Display(Name = "Role")]
            public UserRole Role { get; set; } = UserRole.None;

            [DataType(DataType.Text)]
            [Display(Name = "Group")]
            public string Group { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Faculty number")]
            public string FacultyNumber { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Group = user.Group,
                FacultyNumber = user.FacultyNumber,
                PhoneNumber = phoneNumber
            };

            IList<string> roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                if (role == UserRole.Teacher.ToString())
                {
                    Input.Role = UserRole.Teacher;
                    break;
                }
                else if (role == UserRole.Student.ToString())
                {
                    Input.Role = UserRole.Student;
                    break;
                }
            }         
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (Input.Role != user.Role)
            {
                user.Role = Input.Role;
            }

            if (Input.Group != user.Group)
            {
                user.Group = Input.Group;
            }

            if (Input.FacultyNumber != user.FacultyNumber)
            {
                user.FacultyNumber = Input.FacultyNumber;
            }

            // Handle role edit
            IList<string> roles = await _userManager.GetRolesAsync(user);

            IdentityResult res = null;
            foreach (var role in roles)
            {
                if (role != Input.Role.ToString())
                {
                    if (Input.Role == UserRole.Teacher)
                    {
                        await _userManager.RemoveFromRoleAsync(user, UserRole.Student.ToString());
                        await _userManager.AddToRoleAsync(user, Input.Role.ToString());
                        break;
                    }
                    else if (Input.Role == UserRole.Student)
                    {
                        await _userManager.RemoveFromRoleAsync(user, UserRole.Teacher.ToString());
                        await _userManager.AddToRoleAsync(user, Input.Role.ToString());
                        break;
                    }
                }
            }

            if (res!= null && !res.Succeeded)
            {
                StatusMessage = "Unexpected error when editing user roles.";
                return RedirectToPage();
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
