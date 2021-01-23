using Dropos.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dropos.Startup))]
namespace Dropos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();

        }
        //creating default user roles and admin users for login. 
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //startup is creating first admin role and creating a default admin user
            if (!roleManager.RoleExists ("Admin"))
            {


                //create admin rule
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //this is the maintainer of the website being created below
                var user = new ApplicationUser();
                user.UserName = "Ashley";
                user.Email = "ashley@test.com";
                string userPassword = "TestTest1!";

                var checkUser = UserManager.Create(user, userPassword);

                //add default user to role admin
                if (checkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");
                }


            }
        }
    }
}
