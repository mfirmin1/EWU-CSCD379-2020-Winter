using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public static class SampleData
    {
        //Gift COnstruciton
        public const string Ring = "Ring Doorbell";
        public const string RingUrl = "www.ring.com";
        public const string RingDescription = "The doorbell that saw too much";

        public const string HelloWorld = "HelloWorld";
        public const string HelloWorldUrl = "www.HelloWorld.com";
        public const string HelloWorldDescription = "Create your first Hello World Program for only $99.99 a month";

        public static Gift CreateRingGift() => new Gift(Ring, RingUrl, RingDescription, CreateInigyoMontoya());
        public static Gift CreateHelloWorldScam() => new Gift(HelloWorld, HelloWorldUrl, HelloWorldDescription, CreateBilboBaggins());

        //User Construction
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        public const string InigoMontoya = "imontoya";

        public const string Bilbo = "Bilbo";
        public const string Baggins = "Baggins";
        public const string BilboBaggin = "bbagiins";

        public const string Samwise = "Samwise";
        public const string Gamgee = "Gamgee";
        public const string SamwiseGamgee = "sgamgee";

        public static User CreateInigyoMontoya() => new User(Inigo, Montoya);
        public static User CreateBilboBaggins() => new User(Bilbo, Baggins);
        public static User CreateSamwiseGamgee() => new User(Samwise, Gamgee);

        //Group Construction
        public const string Rohan = "Rohan";
        public const string Gondor = "Gondor";

        static public Group CreateRohan => new Group(Rohan);
        static public Group CreateGondor => new Group(Gondor);
    }
}
