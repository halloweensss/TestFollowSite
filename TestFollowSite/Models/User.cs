using System;
using System.Threading;
using Tests.Helpers;

namespace Tests.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public FromService FromService { get; set; }
        public string NameService { get; set; }
        public Gender Gender { get; set; }

        public static User GetValidUserForLogin()
        {
            return new User()
            {
                Login = "test",
                Email = "test@test.test",
                Password = "testing",
            };
        }

        public static User GetRandomUser()
        {
            return new User()
            {
                Login = TextHelper.GetRandomWord(10),
                Email = TextHelper.GetRandomWord(10) + "@" + TextHelper.GetRandomWord(6) + "." + TextHelper.GetRandomWordWithoutNumbers(2),
                Password = TextHelper.GetRandomWord(8),
                FromService = FromService.None,
                NameService = "",
                Gender = Gender.Other
            };
        }

        public string GetGender()
        {
            switch (this.Gender)
            {
                case Gender.Male:
                    return "man";
                case Gender.Female:
                    return "woman";
                case Gender.Other:
                    return "custom";
                
            }

            return null;
        }

        public string GetServiceName()
        {
            switch (this.FromService)
            {
                case FromService.None:
                    return "noneRadio";
                case FromService.Social:
                    return "socialRadio";
                case FromService.Friends:
                    return "friendsRadio";
                
            }

            return null; 
        }
    }

    public enum FromService
    {
        None,
        Social,
        Friends
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}