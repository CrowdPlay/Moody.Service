using System;
using System.Collections.Generic;
using System.Linq;
using Moody.Models.Data;
using Newtonsoft.Json;
using NUnit.Framework;
using MongoRepository;
using RestSharp;

namespace ClassLibrary1
{
    [TestFixture]
    public class Class1
    {
        private static readonly MongoRepository<Mood> MoodRepository = new MongoRepository<Mood>();
        private const string ApiDomain = "http://thing/";

        [Test]
        public void Thing()
        {
            var jsonStuff = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(stuff);

            foreach (var pair in jsonStuff)
            {
                var mood = new Mood() {Name = pair.Key};
                var tracks = new List<Track>();
                if (MoodRepository.ToList().Find(m => m.Name == pair.Key) != null)
                {
                    foreach (var track in pair.Value)
                    {
                        var parts = track.Split('-');
                        var name = parts.First().Trim();
                        var artist = parts.Last().Trim();

                        //TODO: hit dave's endpoint and get some data
                        var client = new RestClient(ApiDomain);
                        var request = new RestRequest("/search", Method.GET);
                        request.AddParameter("artist", artist);
                        request.AddParameter("name", name);
                        var response = client.Execute(request);
                        dynamic content = JsonConvert.DeserializeObject(response.Content);
                        tracks.Add(new Track()
                        {
                            Duration = TimeSpan.Parse(content.duration),
                            TrackId = int.Parse(content.id)
                        });
                    }
                    mood.TrackInfo = tracks.ToArray();
                    MoodRepository.Update(mood);
                }
            }
            Assert.Pass();
        }

        private const string stuff = @"{  
   'STUDYING':[  
      'Maisie & Neville - David Beats Goliath',
      'Glaciate - Sephari',
      'FRAGILE - Nikonn',
      'Did I Hear You Say Goodbye - TheCoAmSouls',
      'Show Me The Money - Fly Baby'
   ],
   'CALM':[  
      'Maisie & Neville - David Beats Goliath',
      'Less Words - Old Earth',
      'All The Things We Could Have Done - Matteo Righetto',
      'Mt. Wolf-Life Sized Ghost (Rolmex Remix) - Rolmex',
      'FRAGILE - Nikonn'
   ],
   'PIANO':[  
      'a Miserable Heart - Marek Iwaszkiewicz',
      'The Garden - Peder B. Helland',
      'The Dancer - Christopher Ferreira',
      'All The Things We Could Have Done - Matteo Righetto',
      'Erik Satie - Gymnopedie no.1 - Nicholas Smith (Pianist)'
   ],
   'HAPPY':[  
      'Wonders - MedMac',
      'FRAGILE - Nikonn',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'The Dancer - Christopher Ferreira',
      'Heavy Dreams - Häxeri'
   ],
   'CLASSICAL':[  
      '183 Times - Greg Haines',
      'Light Dance (with violin) - akira kosemura',
      'Beatrice - Christopher Ferreira',
      'Moonlight Sonata - Ludwig van Beethoven',
      'October - Christopher Ferreira'
   ],
   'DREAMY':[  
      'Maisie & Neville - David Beats Goliath',
      'Walking On The Clouds - Nikonn',
      'Biomechanical Jive - James Harmon',
      'FRAGILE - Nikonn',
      'For Distant Viewing - Little Tybee'
   ],
   'GROOVY':[  
      'Dance Across The Bass - Slynk',
      'Funkillo - Somefeel',
      'M.Brain - Black Heart [Stooshe edit] - M.Brain',
      'Dance Dark at the Dead Disco - Graveyard Love',
      'Midnite on Pearl Beach - No Mystery In That - Midnite On Pearl Beach'
   ],
   'SEXY':[  
      'Heavy Dreams - Häxeri',
      'Show Me The Money - Fly Baby',
      'Gratify (Dirty Version) - Ninety Vines',
      'Trust Your Senses - Seyffert',
      'Seyffert - Trust Your Senses (Pol Domit Remix) - Pol Domit'
   ],
   'JUST WOKE UP':[  
      'Resting Eyes - Love',
      'All The Things We Could Have Done - Matteo Righetto',
      'Morton\'s Fork - Typhoon',
      'Baba O\'Riley (Kiely Rich Remix) - The Who',
      'How You Got That Girl - Ex Hex'
   ],
   'SECOND CHANCE':[  
      'Tu Vuò Fà L\'Americano - Trio Manouche',
      'Quiero Suerte (Get Lucky - Christopher Dutch Edit\'s) - Daft Kumbya',
      'Shoot the Moon - The Major Pins',
      'Lover, You Should\'ve Come Over • Jeff Buckley cover • - The Night VI',
      'I Walk The Line (Alkalino rework) - Johnny Cash'
   ],
   'SAD':[  
      'a Miserable Heart - Marek Iwaszkiewicz',
      'Let You Down - Grace And Tony',
      'Maisie & Neville - David Beats Goliath',
      'FRAGILE - Nikonn',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow'
   ],
   'MELANCHOLY':[  
      'Through The Light - Disturbia JK',
      'Post Script - Typhoon',
      'The Dancer - Christopher Ferreira',
      'Burning Bushes - VKNK',
      'Walking On The Clouds - Nikonn'
   ],
   'READING':[  
      'What A Wonderful Place This Earth Would Be - Have The Moskovik',
      'Glaciate - Sephari',
      'Fate is what remains - Th.e n.d',
      'FRAGILE - Nikonn',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow'
   ],
   'FUNKY':[  
      'Dance Across The Bass - Slynk',
      'Seven Nation Army - Ben l\'Oncle Soul',
      'Post Script - Typhoon',
      'We Funk This Party Out Feat. Grandmaster Flash, Mele Mel - afrika bambaataa',
      'Awesome - MedMac'
   ],
   'SLEEPY':[  
      'a Miserable Heart - Marek Iwaszkiewicz',
      'FRAGILE - Nikonn',
      'Nevergreen - Emancipator',
      'The Dancer - Christopher Ferreira',
      'All The Things We Could Have Done - Matteo Righetto'
   ],
   'IT\'S RAINING':[  
      'Skinny Love - Bon Iver',
      'Let it Fall - Void Pedal',
      'Innocent Sorrow - Marek Iwaszkiewicz',
      'Walking On The Clouds - Nikonn',
      'EL MAR POR VENIR - POL▲RIS'
   ],
   'CHILLOUT':[  
      'Walking On The Clouds - Nikonn',
      'Alt-J Breezeblocks (Greeb Remix) - Greebli',
      'Show Me The Money - Fly Baby',
      'Nina Simone - nicest',
      'Housemate - Always here for you (Greeb Remix) (Unmastered) - Greeb'
   ],
   'FEMALE':[  
      'Dance Apocalyptic - Janelle Monae',
      'Empty Vicious Nights - Charity Children',
      'Single Ladies (In Mayberry) - Party Ben',
      'Fall (feat. Cuushe) - Populous',
      'Inside - Nikonn'
   ],
   'WORKING':[  
      'Maisie & Neville - David Beats Goliath',
      'Show Me The Money - Fly Baby',
      'Teatrale - Tnks',
      'Did I Hear You Say Goodbye - TheCoAmSouls',
      'Heavy Dreams - Häxeri'
   ],
   'IN LOVE':[  
      'FRAGILE - Nikonn',
      'Maisie & Neville - David Beats Goliath',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'Playground Romance - metic',
      '愛 (Intro) - Funny Death'
   ],
   'SUNDAY MORNING':[  
      'Silver - Caribou',
      'How You Got That Girl - Ex Hex',
      'What A Wonderful Place This Earth Would Be - Have The Moskovik',
      'Blue Moon - Beck',
      'This Time Tomorrow - The Kinks'
   ],
   'RELAX':[  
      'Memories [dunkelbunt Remix] feat Jimi D - Waldeck',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'Warm Winter\'s Day - Alison Valentine',
      'FRAGILE - Nikonn',
      'Skinny Love - Bon Iver'
   ],
   'BEAUTIFUL':[  
      '183 Times - Greg Haines',
      'The Edge - Eugen Kid',
      'cosmic perspective - simpel',
      'Across The Water - Vashti Bunyan',
      'Blue Marine - L.U.C.A.'
   ],
   'ENERGETIC':[  
      'Maisie & Neville - David Beats Goliath',
      'Resting Eyes - Love',
      'a Miserable Heart - Marek Iwaszkiewicz',
      'Dance Apocalyptic - Janelle Monae',
      'England - Kid North'
   ],
   'SUNNY DAY':[  
      'Wasted Time (feat. Ashley Garcia) - FEAR CLUB',
      'into the sun - simpel',
      'new beginning - simpel',
      'Fly A Kite - Paper Chain People',
      'Sun Blows Up Today - The Flaming Lips'
   ],
   'NEED OF LOVE':[  
      'FRAGILE - Nikonn',
      'LENNY KRAVITZ - BELIEVE (SKT REMIX) - sk∆itītājs',
      'Missing Your Love - Willie Johnson Jr.',
      'Shoot the Moon - The Major Pins',
      'Luminous Blue - Last Lynx'
   ],
   'LOST IN THOUGHT':[  
      'Walking On The Clouds - Nikonn',
      'Fate is what remains - Th.e n.d',
      'Through The Light - Disturbia JK',
      'Carmen on Gold St. - Archie Pelago',
      'Post Script - Typhoon'
   ],
   'BALKANKAN':[  
      'Memories [dunkelbunt Remix] feat Jimi D - Waldeck',
      'Woipa Woipa (DJ Click remix) - Nadya Golski',
      'Party Vodka - SKA\'N\'SKA',
      'Ajde Baba - Niko Ne Zna',
      'BABO AMERiKANAC (CARNEVAL Edit) - Dj Tomahawk'
   ],
   'VALENTINE\'S DAY':[  
      'Let\'s Stay Together (Al Green cover) - Fleet Foxes Sing',
      'I Got You Babe (Sonny & Cher cover) - Aidan Moffat & The Best Of\'s',
      'I\'ve Been Thinking - Handsome Boy Modeling School feat Cat Powers',
      'Fade Into You (Mazzy Star Cover) - Jackie',
      'northern sky - Nick Drake'
   ],
   'JAZZY':[  
      'When She\'s Gone - Sam Sullivan Beats',
      'Una giornata ordinaria - Annalisa Marra e Stefano Petucco',
      'El Caminante - Maic',
      'All About Rosie - George Russell',
      'Blue in green - The STQ Feat. Ms. King'
   ],
   'NIGHT DRIVE':[  
      'Dance Dark at the Dead Disco - Graveyard Love',
      'Noizt - Emperia',
      'No Safe Harbor - Wacky Southern Current',
      'Dissociation EP - Gelatinous',
      'The Bottle Episode - Mindil beach markets'
   ],
   'INSTRUMENTAL':[  
      'FRAGILE - Nikonn',
      '183 Times - Greg Haines',
      'When She\'s Gone - Sam Sullivan Beats',
      'Fate is what remains - Th.e n.d',
      'What A Wonderful Place This Earth Would Be - Have The Moskovik'
   ],
   'SEX':[  
      'Feel Me Now - ▲VideoTape',
      'Heavy Dreams - Häxeri',
      'Bridges burning - Bigudi',
      'Dance Across The Bass - Slynk',
      'Acoustic Blues - Alex_Borg'
   ],
   'RUNNING':[  
      'JOHNNIE BUNO - TXTNG - JOHNNIE BUNO',
      'Selector (Fotonovella remix) - Kid Moxie',
      'Oxygen - Damniel',
      'Carried Away - Passion Pit',
      'The Big Hop - Viola Dust'
   ],
   'ROMANTIC':[  
      'Let You Down - Grace And Tony',
      'Feel Me Now - ▲VideoTape',
      'My Heatt Is Burning For You (passion Mix) - Doobie Wainwright',
      'Viviana - Rehlaender',
      'I\'ve Been Thinking - Handsome Boy Modeling School feat Cat Powers'
   ],
   'LOST IN JAMAICA':[  
      'Memories [dunkelbunt Remix] feat Jimi D - Waldeck',
      'Blaze A Fire (Ft. Rasta Rueben) - Dubmatix',
      'Humanity (Love the Way It Should Be) - John Legend & The Roots',
      'Freestyle Ruf Cut (XeRoots Prod.) - Dan Tafari',
      'Shearing You - Prince Buster'
   ],
   'WRITING':[  
      'Nevergreen - Emancipator',
      'Memories [dunkelbunt Remix] feat Jimi D - Waldeck',
      'Lit From Underneath - Andrew Bird',
      'Loungelover - digital dawn',
      'Ethio Invention No. 2 - Andrew Bird'
   ],
   'COOKING TIME':[  
      'Your Girl - Kakkmaddafakka',
      'Honestly - TheShallowsUK',
      'The Swim - Zem Segas',
      'THE WEATHERMAN - David Beats Goliath',
      'Clyps & Bridget Barkan \'Pay Me\' (Video Edit) - DJ E'
   ],
   '8BIT':[  
      'Fly - Bob Picard',
      'Down Back Kick - V-Axys',
      'Gold (Snakehips Bootleg) - Bondax',
      'M.A.A.D. City (Eprom Remix) - Kendrick Lamar',
      'Get Lucky 8 Bits - Wootage'
   ],
   'LET\'S PARTY':[  
      'Dance Across The Bass - Slynk',
      'Dance Dark at the Dead Disco - Graveyard Love',
      'Smooth Criminal - Alien Ant Farm',
      'Kaolo - Yellow Claw',
      'Stomp! - Brothers Johnson'
   ],
   'ASLEEP ON MY FEET':[  
      'FRAGILE - Nikonn',
      'Across The Water - Vashti Bunyan',
      'Conjure - Sleepywolf',
      '​Boxer - Teishi​-1',
      'Back in the Forest (Shinya Mix) - Dystopia'
   ],
   'INSPIRE':[  
      'Es Tres - Maic',
      'Greenwich Mean Time - metic',
      'House boat VS. Too many T\'s - Grant Lazlo',
      'The Socialites (Joe Goddard Remix) - Dirty Projectors',
      'Don\'t Slam the Radio - Nick Festari'
   ],
   'GOOD KARMA':[  
      'For Distant Viewing - Little Tybee',
      'Dance Apocalyptic - Janelle Monae',
      'Eixa parei hapia gia na kimitho - lemonostifel',
      'Daytime Roofies - Jagan Mai',
      'Wonderful World (Dj Twister Edit) - Otis Redding'
   ],
   'OPTIMISTIC':[  
      'For Distant Viewing - Little Tybee',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'Baby\'s Got It - Maylee Todd',
      'THE WEATHERMAN - David Beats Goliath',
      'Elizabeth - Charity Children'
   ],
   'UNTROUBLED':[  
      'Get Lucky (Radio Edit) - Daft Punk',
      'echo room - Bored Nothing',
      'Lone Star - Mirah',
      'Be Happy - Fleisis',
      'Cee-lo Stomp - Black Spider Stomp'
   ],
   'DIRTY':[  
      'Stripper - Sohodolls',
      'Bridges burning - Bigudi',
      'PROPHECY - Chrome',
      'Alarm - Stratus',
      'Hyper Worm Tamer (UNKLE remix) - Grinderman'
   ],
   'SWING':[  
      'Memories [dunkelbunt Remix] feat Jimi D - Waldeck',
      'Can\'t Stop Me - ProleteR',
      'Via Con Me - Swingrowers',
      'Forever & ever - Boogie Belgique',
      'Down South - I.N.Phonium'
   ],
   'LONE STAR':[  
      'No Pussy Blues - Grinderman',
      'Lone Star - Mirah',
      'Human Heart - The Blue Choir',
      'Giving up on love - Slow Club',
      'Distant Dreams and Morning Stars - John White'
   ],
   'SHOWER':[  
      'Heavy Dreams - Häxeri',
      'Out of My League - Fitz And The Tantrums',
      'My Dream Mon Amour - The Blisters Boyz',
      'This Is A Slow Ride - Branson Hollis',
      'Magic Wand - Hippy'
   ],
   'THINKING':[  
      'FRAGILE - Nikonn',
      '梦境 (Dreamland) - 范世琪 (Fan Shiqi)',
      'Nude Remix - Radiohead - James Harmon - James Harmon',
      'Carmen on Gold St. - Archie Pelago',
      'Untitled [outtake from Uncertain Summer] - Wacky Southern Current'
   ],
   'URBAN':[  
      'She Gold - GO DARK',
      '2 On / Thotful (Remix) (feat. Tinashe) - Drake',
      'Who Dat(Falcons Vs Saints 4Ever) ft. Curren$y - Trinidad Jame$',
      'Never Catch Me - Flying Lotus Ft. Kendrick Lamar',
      'Lords Of Death - Baconhead'
   ],
   'EPIC':[  
      'Set To Stun - Ninety Vines',
      'DOREY THE WISE - Tidal Wave - Dorey The Wise',
      'Limits Of Belief - Nick Zagorsky',
      'FRAGILE - Nikonn',
      'Jezebel - Anna Calvi'
   ],
   'SPRING':[  
      'For Distant Viewing - Little Tybee',
      'Carried Away - Passion Pit',
      'I\'ve Got Something I Can Laugh About - Ash Reiter',
      'Morton\'s Fork - Typhoon',
      'Nauseous - codielalonde'
   ],
   'PURA VIDA':[  
      'Cumbia Árabe - Grupo Efecto Sonidero',
      'Keep Walking - MSK.fm',
      'Shingaling - panama cardoon',
      'Chambacu-Aurita Castillo (rebajada) - Sonido Sonidero',
      'Work It En Cumbia (Sam Redmore Mashup) - Missy Elliott'
   ],
   'BERLIN CALLING':[  
      'Acid Lake - Le Marchand de Sable',
      'PPP - Le Marchand de Sable',
      'Elizabeth - Charity Children',
      'Sad Parade - Monkey Records',
      'It\'s Everywhere - Rumpistol & John LaMonica'
   ],
   'PUMP UP THE VOLUME':[  
      'Metal Health (Bang Your Head) - Quiet Riot',
      'Monuments - Two Trick Horse',
      'Set It Off (TWRK Remix) - Diplo & Lazerdisk Party Sex',
      'Slipknot - Duality - Sosna28',
      'True Love (Jeremy Greenspan Remix) - Montag'
   ],
   'DIGITAL':[  
      'One Night In Tokyo - Inner Cult',
      'Carmen on Gold St. - Archie Pelago',
      'Seyffert - Trust Your Senses (Pol Domit Remix) - Pol Domit',
      'Let it Fall - Void Pedal',
      'Mountains - Indian Wells'
   ],
   'COOL':[  
      'Tale Of A Hero - The Lone Drone',
      'Get Lucky (Radio Edit) - Daft Punk',
      'Dance Dark at the Dead Disco - Graveyard Love',
      'Show Me The Money - Fly Baby',
      'Fade Away (written by Eric Borgos) - Doobie Wainwright'
   ],
   'SPRING BREAK':[  
      'When I Dream (Pacific Air Remix) - Ra Ra Riot',
      'We Are Electric ft. Simon Wilcox - DVBBS',
      'Distance - Beaches',
      'Recovery - Frank Turner',
      'San Francisco - Foxygen'
   ],
   'BEACH PARTY':[  
      'Blue Marine - L.U.C.A.',
      'Part of You - ASEA',
      'Clyps & Bridget Barkan \'Pay Me\' (Video Edit) - DJ E',
      'Held (Fort Romeau Remix) - HOLY OTHER',
      'Envision (ft. Channy From Polica) - Supreme Cuts'
   ],
   'NEW YORK NEW YORK':[  
      'New York Nights - Indian Wells',
      'Ancient Ways - Interpol',
      'Frantic touches, lucid dreams - GLEAM',
      'The Message - Grandmaster Flash',
      'New York Is Killing Me (featuring Nas) - Gil Scott-Heron'
   ],
   'DRESSING UP':[  
      'Single Ladies (In Mayberry) - Party Ben',
      'I Can Tell (By The Way You Move) - George Fitzgerald',
      'Sharks - Cam Meekins',
      'You\'re all I need to get by • Marvin Gaye Cover • - The Night VI',
      'PILGRIM (MS MR REMIX) - MØ'
   ],
   'PAINTING':[  
      'Chemistry - Groove Squared',
      'What A Wonderful Place This Earth Would Be - Have The Moskovik',
      '梦境 (Dreamland) - 范世琪 (Fan Shiqi)',
      'OPE - James Harmon',
      'Just Passing - James Harmon'
   ],
   'MAKE LOVE':[  
      'Bridges burning - Bigudi',
      'I\'ve Been Thinking - Handsome Boy Modeling School feat Cat Powers',
      'Follow My Lead (Explicit Version) - Matt Duart',
      'Slow - Sun Glitters (feat. Sara)',
      'Angel Dust - Gil Scott-Heron'
   ],
   'DISH WASHING':[  
      'Fake Tears (Gordon Voidwell Remix) - TECLA',
      'Dried Out Cities - Fallulah',
      'Laundry - Say Hi',
      'Car Wash (7\' Version) - Rose Royce',
      'Echo and The Empress - Balloon'
   ],
   'FEEL LIKE CRYING':[  
      'Lost Property - Emily Plays',
      'Don\'t Slam the Radio - Nick Festari',
      'Wish You Were Here (Pink Floyd Cover) - Sparklehorse',
      'Tallulah - Sonata Arctica',
      'Oh Darling - Tom Odell'
   ],
   'WINTER':[  
      'A Jagged Gorgeous Winter (Album Mix) - The Main Drag',
      'Warm Winter\'s Day - Alison Valentine',
      'New York Nights - Indian Wells',
      'Arquitecto - Maic',
      'Blue Marine - L.U.C.A.'
   ],
   'GAMING':[  
      'Kishi s Flying Circus (On The Tower OST) - weirddreams',
      'Song of Storms Remix - Legend of Zelda - TheOfficialEnox',
      'Light Dance (with violin) - akira kosemura',
      'Meltdown (Squarepusher vs Ghostpoet Version) - Ghostpoet',
      'M.A.A.D. City (Eprom Remix) - Kendrick Lamar'
   ],
   'SITTING ON THE TOILET':[  
      'Silence Broken - Peter Buffett',
      'Love Delay - Kirin J Callinan',
      'I Wanna Dance (But I Dont Know How) - SKATERS',
      'I\'ve Got That Tune - chinese man',
      'Easy Easy - King Krule'
   ],
   'ROAD TRIP':[  
      'echo room - Bored Nothing',
      'Sun In My Bones - windings',
      'Powder - Gengahr',
      'Memories [dunkelbunt Remix] feat Jimi D - Waldeck',
      'Away / Towards - Hookworms'
   ],
   'PEACEFUL':[  
      'FRAGILE - Nikonn',
      'Maisie & Neville - David Beats Goliath',
      'Less Words - Old Earth',
      '梦境 (Dreamland) - 范世琪 (Fan Shiqi)',
      'Wasted Time (feat. Ashley Garcia) - FEAR CLUB'
   ],
   'DOODLING':[  
      'Nevergreen - Emancipator',
      'The Bottle Episode - Mindil beach markets',
      'In My Guts - Bumpkin Island',
      'Shinnan - K-conjog',
      'Come to my party - Snippet'
   ],
   'MEDITATION':[  
      'Doodle #3 (Breathe) - Piano\'s Grace',
      'a Miserable Heart - Marek Iwaszkiewicz',
      'Concentr (panduminium) - Umin',
      'Driving the yellow car - Th.e n.d',
      'Ngam City - Th.e n.d'
   ],
   'LONELY':[  
      'a Miserable Heart - Marek Iwaszkiewicz',
      'FRAGILE - Nikonn',
      'The Storm In My Heart - Deadbios',
      'No Pussy Blues - Grinderman',
      '183 Times - Greg Haines'
   ],
   'SUMMER':[  
      'Tyrian\'s Plea - Dylan Fuller',
      'Garden - PANTHER MARTIN',
      'Get Lucky (Radio Edit) - Daft Punk',
      'England - Kid North',
      'Deep Tone - Dylan Fuller'
   ],
   'DANCING':[  
      'Stomp! - Brothers Johnson',
      'Dance Apocalyptic - Janelle Monae',
      'RELOAD (ELECTRO  HOUSE )::DJay#7:: - Jay Panchal (DJay#7 RMX\')',
      'Get Lucky (Radio Edit) - Daft Punk',
      'Softly death - Lúnatic'
   ],
   'TANGO LESSON':[  
      'Nostalgias - Annalisa Marra e Stefano Petucco',
      'No Man\'s Land - Riva Starr Feat. Carmen Consoli',
      'Satori - Rodrigo y Gabriela',
      'Habar n-ai tu from \'      DIVINE\' - Oana Catalina Chitu',
      'Tango \'till I\'m Sore - Luca Dell\'Anna'
   ],
   'SWEET':[  
      'FRAGILE - Nikonn',
      'Resting Eyes - Love',
      'Elizabeth - Charity Children',
      'Warm Winter\'s Day - Alison Valentine',
      'Born in the 80s - The Numbers'
   ],
   'CANDLELIT DINNER':[  
      'The Next Time We Dance - metic',
      'Let\'s Stay Together (Al Green cover) - Fleet Foxes Sing',
      'La meraviglia - Roberto Dell\'Era',
      'Make Love Stay - Secret Mountains',
      'In Your Eyes (Peter Gabriel Cover) - BANKS'
   ],
   'DRINKING':[  
      'Dance Dark at the Dead Disco - Graveyard Love',
      'Party Vodka - SKA\'N\'SKA',
      'Valentine ft. Kyiki (Kyogi Remix) - DE$iGNATED',
      'You Need Me On My Own - Totally Enormous Extinct Dinosaurs',
      'Emerald City - The Tossers'
   ],
   'CHILL':[  
      'Maisie & Neville - David Beats Goliath',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'Socket - Ninety Vines',
      'Less Words - Old Earth',
      'Nevergreen - Emancipator'
   ],
   'DRAWING':[  
      'The Bottle Episode - Mindil beach markets',
      'Here - Syron',
      'Mas Que Nada (DjRebel re-edit) - Tamba Trio',
      'Break In - Saturday Looks Good To Me',
      'White Light (Wilco Acoustic Live Cover) - albertoarcangeli'
   ],
   'POOL PARTY':[  
      'So will be now... (Club Revision) - John Talabot',
      'Something About The Fire (Carlos Serrano Mix) - Adele VS Daft Punk',
      'Naughty (Cameo Culture Remix) - Name In Lights',
      'Bellz - Jensen Sportag',
      'Travellin Man (Bruce Missile Blend) - Mos Def & Djtzinas'
   ],
   'DUBSTEP':[  
      'Alarm - Stratus',
      'Extended GuitarStep - Nick Zagorsky',
      'Stealth of light - Casey Cat',
      'Set It Off (TWRK Remix) - Diplo & Lazerdisk Party Sex',
      'Desert Queen - Idea Unsound'
   ],
   'VINTAGE':[  
      'Not The Last© - Lúnatic',
      'Eleonore - Turtles',
      'Let\'s Dance (Jean Claude Gavri Les Dance Re Edit) - Jean Claude Gavri',
      'Deanna - Nick Cave & The Bad Seeds',
      'Lords Of Death - Baconhead'
   ],
   'TIRED':[  
      'All The Things We Could Have Done - Matteo Righetto',
      'Resting Eyes - Love',
      'a Miserable Heart - Marek Iwaszkiewicz',
      'Memories [dunkelbunt Remix] feat Jimi D - Waldeck',
      'Dissociation EP - Gelatinous'
   ],
   'AUTUMN':[  
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'The Siege - giorgio giampà',
      'basta melalcoholic (demo 2010) - Nauris Brūvelis',
      'Let it Fall - Void Pedal',
      'Autumn - Airy Fizz'
   ],
   'FLYING AWAY':[  
      'Walking On The Clouds - Nikonn',
      '04 Slow Peels - Com Truise',
      'Nova - Four Tet + Burial',
      'Get Lucky (feat. Pharrell Williams) (Coachella Extended version) - Daft Punk',
      'Speed of Dark (Andrew Weatherall Remix) - Emiliana Torrini'
   ],
   'YOGA':[  
      'Maisie & Neville - David Beats Goliath',
      'Dusk - The Meditone Project',
      'Ngam City - Th.e n.d',
      'Driving the yellow car - Th.e n.d',
      'Killing Time - tAsMo'
   ],
   'SPACE TRIP':[  
      'Arquitecto - Maic',
      'Paris in white powder - Karov',
      'Dubi1queno2 Exp - Smadj',
      'Hidden Scenes (Almeeva Remix) - SUNDRUGS',
      'Slow - Sun Glitters (feat. Sara)'
   ],
   'HEARTBROKEN':[  
      'a Miserable Heart - Marek Iwaszkiewicz',
      'A Flower - The Rag Trade',
      'Inside - Nikonn',
      'Walking On The Clouds - Nikonn',
      'Time Warp - The Rocky Horror Picture Show'
   ],
   'DOING LAUNDRY':[  
      'Break In - Saturday Looks Good To Me',
      'Devil Inside Me - Matt Berry',
      'Dried Out Cities - Fallulah',
      'Echo and The Empress - Balloon',
      'Bleu Iodure - Bob Picard'
   ],
   'LIKE A CHILD':[  
      'Carried Away - Passion Pit',
      'Popples - Lapingra',
      'Better Days - Edward Sharpe And The Magnetic Zeros',
      'Whale Inhale - Aimée Portioli',
      'Elizabeth - Charity Children'
   ],
   'CLASSIC':[  
      'Maisie & Neville - David Beats Goliath',
      'Part II c - Keith Jarrett',
      'Svefn-g-englar - Vitamin Sting Quartet Performs Sigur Rós',
      'Fate is what remains - Th.e n.d',
      'Let\'s Dance (Jean Claude Gavri Les Dance Re Edit) - Jean Claude Gavri'
   ],
   'ELEGANT':[  
      'Walking On The Clouds - Nikonn',
      'Light Dance (with violin) - akira kosemura',
      'Part II c - Keith Jarrett',
      'All About Rosie - George Russell',
      'Inside - Nikonn'
   ],
   'MASHUP':[  
      'Get Lucky (Shay Palti vs  Willy Wlliam  Mashup) - Daft Punk',
      'Single Ladies (In Mayberry) - Party Ben',
      'Something About The Fire (Carlos Serrano Mix) - Adele VS Daft Punk',
      'Work It En Cumbia (Sam Redmore Mashup) - Missy Elliott',
      'The Robbery Anthem Song (Onra Vs. Tha Trickaz) - Senbeï'
   ],
   'FREE':[  
      'For Distant Viewing - Little Tybee',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'Sun In My Bones - windings',
      'Walking On The Clouds - Nikonn',
      'Out of My League - Fitz And The Tantrums'
   ],
   'INSPIRED':[  
      'FRAGILE - Nikonn',
      'Glassblower - Bob Picard',
      'Here and Gone - Tipsheda',
      'Dante\'s Riddle - Pip John',
      'Dawn of Hope - Peder B. Helland'
   ],
   'COMMUTING':[  
      'we\'re heading home - Set In Sand',
      'Dumb Ways To Die - Tangerine Kitty',
      'Cara Falsa - OMBRE',
      'Waiting - Oi Va Voi'
   ],
   'CINEMA':[  
      'Time Warp - The Rocky Horror Picture Show',
      'Call Me in the Day - La Luz',
      'The Mighty Rio Grande - This Will Destroy You',
      'Museum Motel - Kid Moxie & The Gaslamp Killer',
      'Lo Chiamavano King (His Name is King) - Luis Bacalov'
   ],
   'HOMEWORK':[  
      'Did I Hear You Say Goodbye - TheCoAmSouls',
      'FRAGILE - Nikonn',
      'Glassblower - Bob Picard',
      'Doin\' It Blue - Sam Sullivan Beats',
      'Doodle #3 (Breathe) - Piano\'s Grace'
   ],
   'CUTE':[  
      'FRAGILE - Nikonn',
      'Walking On The Clouds - Nikonn',
      'El Caminante - Maic',
      'Not The Last© - Lúnatic',
      'Kidz - Lúnatic'
   ],
   'HIGH':[  
      'Stealth of light - Casey Cat',
      'FRAGILE - Nikonn',
      'Blaze A Fire (Ft. Rasta Rueben) - Dubmatix',
      'Kassiopea - Naive Diver',
      '01 - Don Goliath - Fittest Of The Fittest (2mins Preview) - Don Goliath'
   ],
   'DEPRESSED':[  
      'FRAGILE - Nikonn',
      '183 Times - Greg Haines',
      'a Miserable Heart - Marek Iwaszkiewicz',
      'Less Words - Old Earth',
      'Lost Property - Emily Plays'
   ],
   'REVOLUTION':[  
      'Heretic\'s Hymn - Double Dagger',
      'Radioactive - Imagine Dragons',
      'Elizabeth - Charity Children',
      'Revolution (Riddim Tuffa Remix) - Shanti D',
      'The Rain Comes Again - CAYETANO'
   ],
   'WORK':[  
      'Monuments - Two Trick Horse',
      'Higher Love ( NineOne remix ) - James Vincent Mc Morrow',
      'Walking On The Clouds - Nikonn',
      'Show Me The Money - Fly Baby',
      'Dance Apocalyptic - Janelle Monae'
   ],
   'JAZZ':[  
      'a Miserable Heart - Marek Iwaszkiewicz',
      'Doin\' It Blue - Sam Sullivan Beats',
      'Tyrian\'s Plea - Dylan Fuller',
      'Valentine ft. Kyiki (Kyogi Remix) - DE$iGNATED',
      'Caí do Céu (Fell of the Heaven) - Nei Zigma'
   ]
}";
    }
}
