// See https://aka.ms/new-console-template for more information

using CommonModelsLib.Contexts;
using CommonModelsLib;
using SecureBackEndAuthorizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

while (true)
{
    var connectionString = "server=localhost;user=root;password=sa;database=SkripsiBos";
    var serverVersion = new MySqlServerVersion(new Version(10, 11, 3));
    UserContext.options =
        new DbContextOptionsBuilder<UserContext>().UseMySql(connectionString, serverVersion, a => a.MigrationsAssembly("BEAPIDemo")).Options;

    //Console.WriteLine("Hello, World!");
    var username = "Jamiel123";
    var user = await UserContext.GetUserBaseByUserName(username);
    if (user == null)
        Console.WriteLine("null");

    //Console.WriteLine(StaticOTP.GenerateOTP(username));
    string a = Console.ReadLine();
    Console.WriteLine(StaticOTP.VerifyOTP(username, a).ToString());

}

