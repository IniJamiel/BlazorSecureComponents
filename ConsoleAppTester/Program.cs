// See https://aka.ms/new-console-template for more information

using SecureBackEndAuthorizer;
while (true)
{
    Console.WriteLine("Hello, World!");
    var username = "Jamiel123";

    Console.WriteLine(StaticOTP.GenerateOTP(username));
    string a = Console.ReadLine();
    Console.WriteLine(StaticOTP.VerifyOTP(username, a).ToString());

}

