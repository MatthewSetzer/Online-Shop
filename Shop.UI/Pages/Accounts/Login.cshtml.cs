using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Shop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {

        private SignInManager<IdentityUser> _signInManager;

        //Calling SignInManger to use in scope and sign users in
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Credentials { get; set; }

        public void OnGet()
        {
        }

        //OnPost method to sign in users with inputted username and password
        public async Task<IActionResult> OnPost()
        {
            var result = await _signInManager.PasswordSignInAsync(Credentials.Username, Credentials.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }

    }
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}