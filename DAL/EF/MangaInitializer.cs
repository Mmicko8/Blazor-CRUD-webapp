using System;
using System.Collections.Generic;
using MangaProject.BL.Domain;

namespace MangaProject.DAL.EF
{
    public static class MangaInitializer
    {
        private static Boolean _isInitialized = false;

        internal static void Initialize(MangaDbContext context, bool dropCreateDatabase = false)
        {
            if (_isInitialized)
                return;

            if (dropCreateDatabase)
                context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
                Seed(context);
            _isInitialized = true;
        }

        private static void Seed(MangaDbContext context)
        {
            var tsugumi = new Author("Tsugumi Ohba", new DateTime(1962, 8, 17), Gender.Male);
            var takeshi = new Author("Takeshi Obata", new DateTime(1969, 2, 11), Gender.Male);
            var hiroshi = new Author("Hiroshi Sakurazaka", new DateTime(1970, 12, 28), Gender.Male);
            var ryosuke = new Author("Ryosuke Takeuchi", new DateTime(1980, 11, 20), Gender.Male);

            var eiichiro = new Author("Eiichiro Oda", new DateTime(1970, 1, 1), Gender.Male);
            var hiromu = new Author("Hiromu Arakawa", new DateTime(1973, 3, 8), Gender.Female);
            var kentaro = new Author("Kentaro Miura", new DateTime(1966, 7, 11), Gender.Male);
            var norihiro = new Author("Norihiro Yagi", new DateTime(1968, 4, 24), Gender.Male);
            var naoko = new Author("Naoko Takeuchi", new DateTime(1967, 3, 15), Gender.Female);

            var guts = new Protagonist("Guts", 24, Gender.Male);
            var edward = new Protagonist("Edward Elric", 15, Gender.Male);
            var light = new Protagonist("Light Yagami", 17, Gender.Male);
            var moritaka = new Protagonist("Moritaka Mashiro", 16, Gender.Male);
            var luffy = new Protagonist("Luffy D. Monkey", 17, Gender.Male);
            var keiji = new Protagonist("Keiji Kiriya", 24, Gender.Male);
            var riko = new Protagonist("Riko", 12, Gender.Female);
            var usagi = new Protagonist("Usagi Tsukino", 14, Gender.Female);


            var berserk = new Manga("Berserk", new DateTime(1989, 8, 25), 40, guts, 9.4);
            var fullmetalAlchemist = new Manga("Fullmetal Alchemist", new DateTime(2001, 7, 12),
                27, edward, 9.08);
            var deathNote = new Manga("Death Note", new DateTime(2003, 12, 1), 12,
                light, 8.71);
            var bakuman =
                new Manga("Bakuman.", new DateTime(2008, 8, 11), 20, moritaka,
                    8.42); // <Bakuman.> is the title, not <Bakuman>
            var onePiece = new Manga("One Piece", new DateTime(1997, 7, 22), 100, luffy,
                9.16);
            var allYouNeedIsKill = new Manga("All You Need Is Kill", new DateTime(2014, 1, 9),
                2, keiji, 7.85);
            var madeInAbyss = new Manga("Made in Abyss", new DateTime(2012, 10, 20), 10, riko,
                8.81);
            var sailorMoon = new Manga("Sailor Moon", new DateTime(1991, 2, 3), 18, usagi,
                null);


            var youngAnimal = new Magazine("Young Animal", Schedule.Monthly, new List<Manga> {berserk});
            var shonenJump =
                new Magazine("Weekly Shonen Jump", Schedule.Weekly, new List<Manga> {deathNote, bakuman, onePiece});
            var youngJump =
                new Magazine("Weekly Young Jump", Schedule.Weekly, new List<Manga> {allYouNeedIsKill});
            var gangan = new Magazine("Monthly Shonen Gangan", Schedule.Monthly,
                new List<Manga> {fullmetalAlchemist});
            var gamma = new Magazine("Web Comic Gamma", Schedule.Other, new List<Manga>() {madeInAbyss});
            var nakayoshi = new Magazine("Nakayoshi", Schedule.Monthly, new List<Manga>() {sailorMoon});

            berserk.Magazine = youngAnimal;
            fullmetalAlchemist.Magazine = gangan;
            deathNote.Magazine = shonenJump;
            bakuman.Magazine = shonenJump;
            onePiece.Magazine = shonenJump;
            allYouNeedIsKill.Magazine = youngJump;
            madeInAbyss.Magazine = gamma;
            sailorMoon.Magazine = nakayoshi;


            var berserkKentaro = new MangaAuthor(ContributionType.Both, berserk, kentaro);
            var fullmetalHiromu = new MangaAuthor(ContributionType.Both, fullmetalAlchemist, hiromu);
            var onePieceEiichiro = new MangaAuthor(ContributionType.Both, onePiece, eiichiro);
            var madeInAbyssNorihiro = new MangaAuthor(ContributionType.Both, madeInAbyss, norihiro);
            var sailorMoonNaoko = new MangaAuthor(ContributionType.Both, sailorMoon, naoko);

            var deathNoteTsugumi = new MangaAuthor(ContributionType.Story, deathNote, tsugumi);
            var deathNoteTakeshi = new MangaAuthor(ContributionType.Art, deathNote, takeshi);
            var bakumanTsugumi = new MangaAuthor(ContributionType.Story, bakuman, tsugumi);
            var bakumanTakeshi = new MangaAuthor(ContributionType.Art, bakuman, takeshi);

            var allYouNeedTakeshi = new MangaAuthor(ContributionType.Art, allYouNeedIsKill, takeshi);
            var allYouNeedHiroshi = new MangaAuthor(ContributionType.Story, allYouNeedIsKill, hiroshi);
            var allYouNeedRyosuke = new MangaAuthor(ContributionType.Story, allYouNeedIsKill, ryosuke);

            berserk.Authors = new List<MangaAuthor> {berserkKentaro};
            fullmetalAlchemist.Authors = new List<MangaAuthor> {fullmetalHiromu};
            deathNote.Authors = new List<MangaAuthor> {deathNoteTsugumi, deathNoteTakeshi};
            bakuman.Authors = new List<MangaAuthor> {bakumanTsugumi, bakumanTakeshi};
            onePiece.Authors = new List<MangaAuthor> {onePieceEiichiro};
            allYouNeedIsKill.Authors = new List<MangaAuthor> {allYouNeedTakeshi, allYouNeedHiroshi, allYouNeedRyosuke};
            madeInAbyss.Authors = new List<MangaAuthor> {madeInAbyssNorihiro};
            sailorMoon.Authors = new List<MangaAuthor> {sailorMoonNaoko};

            kentaro.Mangas = new List<MangaAuthor> {berserkKentaro};
            hiromu.Mangas = new List<MangaAuthor> {fullmetalHiromu};
            tsugumi.Mangas = new List<MangaAuthor> {deathNoteTsugumi, bakumanTsugumi};
            takeshi.Mangas = new List<MangaAuthor> {deathNoteTakeshi, bakumanTakeshi, allYouNeedTakeshi};
            eiichiro.Mangas = new List<MangaAuthor> {onePieceEiichiro};
            hiroshi.Mangas = new List<MangaAuthor> {allYouNeedHiroshi};
            ryosuke.Mangas = new List<MangaAuthor> {allYouNeedRyosuke};
            norihiro.Mangas = new List<MangaAuthor> {madeInAbyssNorihiro};
            naoko.Mangas = new List<MangaAuthor> {sailorMoonNaoko};

            var mangasToAdd = new List<Manga>
                {berserk, fullmetalAlchemist, deathNote, bakuman, onePiece, allYouNeedIsKill, madeInAbyss, sailorMoon};
            foreach (var manga in mangasToAdd)
            {
                context.Mangas.Add(manga);
            }

            var magazinesToAdd = new List<Magazine> {youngAnimal, shonenJump, youngJump, gangan, gamma, nakayoshi};
            foreach (var magazine in magazinesToAdd)
            {
                context.Magazines.Add(magazine);
            }

            var authorsToAdd = new List<Author>
                {tsugumi, takeshi, hiroshi, ryosuke, eiichiro, hiromu, kentaro, norihiro, naoko};
            foreach (var author in authorsToAdd)
            {
                context.Authors.Add(author);
            }

            context.Animes.Add(new Anime("Fairy Tail", 130, 5.0));
            context.Animes.Add(new Anime("Fate Zero", 3, null));
            context.Animes.Add(new Anime("Fate stay night", 24, 6));
            context.Animes.Add(new Anime("Accel World", 24, 6));
            context.Animes.Add(new Anime("Blue Exorcist", 25, 6));
            context.Animes.Add(new Anime("Naruto", 220, 7.0));
            context.Animes.Add(new Anime("Naruto Shippuden", 65, null));
            context.Animes.Add(new Anime("Tengen Toppa Gurren Lagann", 27, 8));
            context.Animes.Add(new Anime("Toradora!", 25, 8));
            context.Animes.Add(new Anime("Daily Lives of High School Boys", 12, 8));
            context.Animes.Add(new Anime("One Piece", 605, null));
            context.Animes.Add(new Anime("D.Gray-man", 14, null));
            context.Animes.Add(new Anime("Shaman King", 6, null));
            context.Animes.Add(new Anime("Kuroko's Basketball", 2, null));
            context.Animes.Add(new Anime("Fullmetal Alchemist", 51, 9.0));
            context.Animes.Add(new Anime("InuYasha", 167, null));
            context.Animes.Add(new Anime("Blood Lad", 10, 6));
            context.Animes.Add(new Anime("Future Diary", 26, 6));
            context.Animes.Add(new Anime("Code Geass: Lelouch of the Rebellion", 25, 8));
            context.Animes.Add(new Anime("Beyblade: Metal Fusion", 51, null));
            context.Animes.Add(new Anime("Bakugan Battle Brawlers", 51, null));
            context.Animes.Add(new Anime("Yu-Gi-Oh! GX", 180, null));
            context.Animes.Add(new Anime("Lovely Complex", 24, 7.0));
            context.Animes.Add(new Anime("Full Metal Panic!", 3, null));
            context.Animes.Add(new Anime("Angel Beats!", 13, 8));
            context.Animes.Add(new Anime("Noragami", 12, 8));
            context.Animes.Add(new Anime("Akame ga Kill!", 24, 6));
            context.Animes.Add(new Anime("Darker than Black", 25, 7.0));
            context.Animes.Add(new Anime("Durarara!!", 24, 7.0));
            context.Animes.Add(new Anime("Guilty Crown", 22, 6));
            context.Animes.Add(new Anime("No Game No Life", 12, 8));
            context.Animes.Add(new Anime("Sword Art Online", 25, 5.0));
            context.Animes.Add(new Anime("Afro Samurai", 5, 7.0));
            context.Animes.Add(new Anime("Afro Samurai Resurrection", 1, 7.0));
            context.Animes.Add(new Anime("History's Strongest Disciple Kenichi", 50, 7.0));
            context.Animes.Add(new Anime("Rurouni Kenshin", 1, null));
            context.Animes.Add(new Anime("Tokyo Ghoul", 12, 6));
            context.Animes.Add(new Anime("Psycho-Pass", 22, 8));
            context.Animes.Add(new Anime("Deadman Wonderland", 3, null));
            context.Animes.Add(new Anime("Attack on Titan: Ilse's Notebook", 3, null));
            context.Animes.Add(new Anime("Attack on Titan", 25, 9.0));
            context.Animes.Add(new Anime("Claymore", 26, 7.0));
            context.Animes.Add(new Anime("Kill La Kill", 1, null));
            context.Animes.Add(new Anime("Black Bullet", 1, null));
            context.Animes.Add(new Anime("Hyouka", 1, null));
            context.Animes.Add(new Anime("Assassination Classroom", 22, 7.0));
            context.Animes.Add(new Anime("Monthly Shoujo Nozaki-kun", 12, 7.0));
            context.Animes.Add(new Anime("Gintama", 4, null));
            context.Animes.Add(new Anime("Spirited Away", 1, 10));
            context.Animes.Add(new Anime("My Neighbor Totoro", 1, 8));
            context.Animes.Add(new Anime("Princess Mononoke", 1, 10));
            context.Animes.Add(new Anime("Howl's Moving Castle", 1, 10));
            context.Animes.Add(new Anime("Nausicaa of the Valley of the Wind", 1, 9.0));
            context.Animes.Add(new Anime("Akira", 1, 8));
            context.Animes.Add(new Anime("Dragon Ball Z", 60, 5.0));
            context.Animes.Add(new Anime("Steins Gate", 6, null));
            context.Animes.Add(new Anime("Great Teacher Onizuka", 43, 8));
            context.Animes.Add(new Anime("Aldnoah Zero", 12, 6));
            context.Animes.Add(new Anime("Ouran High School Host Club", 3, null));
            context.Animes.Add(new Anime("Special A", 2, null));
            context.Animes.Add(new Anime("Attack on Titan: No Regrets", 2, 8));
            context.Animes.Add(new Anime("Eyeshield 21", 6, null));
            context.Animes.Add(new Anime("Eden of the East", 1, null));
            context.Animes.Add(new Anime("Code Geass: Akito the Exiled", 5, 6));
            context.Animes.Add(new Anime("Good Luck Girl!", 13, 7.0));
            context.Animes.Add(new Anime("The Seven Deadly Sins", 3, null));
            context.Animes.Add(new Anime("Tokyo Ghoul root a", 12, 5.0));
            context.Animes.Add(new Anime("Parasyte the maxim", 24, 9.0));
            context.Animes.Add(new Anime("My Little Monster", 13, 7.0));
            context.Animes.Add(new Anime("Death Parade", 23, null));
            context.Animes.Add(new Anime("The Comic Artist and His Assistants", 2, null));
            context.Animes.Add(new Anime("Yowamushi Pedal", 38, 6));
            context.Animes.Add(new Anime("Gintama", 252, 9.3));
            context.Animes.Add(new Anime("Seraph of the End", 12, 5.0));
            context.Animes.Add(new Anime("Hajime no Ippo: Rising", 25, 8));
            context.Animes.Add(new Anime("God Eater", 13, 7.5));
            context.Animes.Add(new Anime("One-Punch Man", 2, null));
            context.Animes.Add(new Anime("Hellsing", 13, 7.0));
            context.Animes.Add(new Anime("Hellsing Ultimate", 10, 8));
            context.Animes.Add(new Anime("My Love Story!!", 24, 7.0));
            context.Animes.Add(new Anime("Hajime no Ippo: The Fighting!", 75, 8));
            context.Animes.Add(new Anime("My Hero Academia", 13, 8));
            context.Animes.Add(new Anime("ERASED", 12, 7.0));
            context.Animes.Add(new Anime("Baccano!", 13, 8.3));
            context.Animes.Add(new Anime("Beyond the Boundary", 12, 6.7));
            context.Animes.Add(new Anime("The Devil is a Part-Timer!", 13, 7.0));
            context.Animes.Add(new Anime("JoJo's Bizarre Adventure", 26, 7.0));
            context.Animes.Add(new Anime("JoJo's Bizarre Adventure: Diamond is Unbreakable", 39, 9.0));
            context.Animes.Add(new Anime("JoJo's Bizarre Adventure: Stardust Crusaders", 24, 8));
            context.Animes.Add(new Anime("Drifters", 12, 6));
            context.Animes.Add(new Anime("Black Lagoon", 12, 7.0));
            context.Animes.Add(new Anime("Mob Psycho 100", 12, 9.0));
            context.Animes.Add(new Anime("Blue Exorcist: Kyoto Saga", 12, 7.0));
            context.Animes.Add(new Anime("The Heroic Legend of Arslan", 25, 7.0));
            context.Animes.Add(new Anime("Re:ZERO: Starting Life in Another World", 25, 7.0));
            context.Animes.Add(new Anime("Saga of Tanya the Evil", 12, 7.0));
            context.Animes.Add(new Anime("Neon Genesis Evangelion", 26, 8));
            context.Animes.Add(new Anime("Cowboy Bebop", 10, 9.1));
            context.Animes.Add(new Anime("Golden Boy", 6, 7.0));
            context.Animes.Add(new Anime("Ajin: Demi-Human", 13, 6));
            context.Animes.Add(new Anime("Grave of the Fireflies", 1, 8.9));
            context.Animes.Add(new Anime("Haikyuu!!", 25, 7.0));
            context.Animes.Add(new Anime("Inuyashiki: Last Hero", 11, 6));
            context.Animes.Add(new Anime("Devilman: Crybaby", 10, 9.0));
            context.Animes.Add(new Anime("Bakemonogatari", 15, 8));
            context.Animes.Add(new Anime("Nisemonogatari", 11, 8));
            context.Animes.Add(new Anime("Hanamonogatari", 5, 7.0));
            context.Animes.Add(new Anime("Tsukimonogatari", 4, 8));
            context.Animes.Add(new Anime("Owarimonogatari", 12, 8));
            context.Animes.Add(new Anime("Pop Team Epic", 1, null));
            context.Animes.Add(new Anime("Megalo Box", 13, 7.0));
            context.Animes.Add(new Anime("Hinamatsuri", 12, 8));
            context.Animes.Add(new Anime("JoJo's Bizarre Adventure: Golden Wind", 39, 9.0));
            context.Animes.Add(new Anime("Grand Blue", 12, 8));
            context.Animes.Add(new Anime("Dororo (2019)", 24, 8));
            context.Animes.Add(new Anime("Vinland Saga", 24, 9.0));
            context.Animes.Add(new Anime("KonoSuba", 24, 8));
            context.Animes.Add(new Anime("Pokemon Advanced", 192, 7.8));
            context.Animes.Add(new Anime("Castlevania", 4, 9.0));
            context.Animes.Add(new Anime("The Helpful Fox Senko-san", 14, 7));
            context.Animes.Add(new Anime("Laputa: Castle in the Sky", 1, 9.0));
            context.Animes.Add(new Anime("Keep Your Hands Off Eizouken!", 12, 8));
            context.Animes.Add(new Anime("Kaguya-sama: Love Is War?", 12, 8));
            context.Animes.Add(new Anime("Little Witch Academia", 25, 8));
            context.Animes.Add(new Anime("The Millionaire Detective", 11, 6));
            context.Animes.Add(new Anime("Great Pretender", 23, null));
            context.Animes.Add(new Anime("Ranking of Kings", 9, 9.0));


            context.SaveChanges();
            context.ChangeTracker.Clear();
        }
    }
}