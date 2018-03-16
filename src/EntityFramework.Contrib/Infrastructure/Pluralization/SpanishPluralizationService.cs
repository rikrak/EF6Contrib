namespace System.Data.Entity.Infrastructure.Pluralization
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Utilities;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///  Pluralization service for spanish language.
    /// </summary>
    public class SpanishPluralizationService
        : IPluralizationService
    {
        BidirectionalDictionary<String, String> _userPluralizations = new BidirectionalDictionary<string, string>();
        BidirectionalDictionary<String, String> _userDictionaryEntries = new BidirectionalDictionary<String, String>();

        StringBidirectionalDictionary _irregularWords = new StringBidirectionalDictionary(new Dictionary<string, string>()
        {
            {"faralá","faralaes"},
            {"albalá","albalaes"},
            {"no","noes"},
            {"gay","gais"},
            {"jersey","jerséis"},
            {"espray","espráis"},
            {"yóquey","yoqueis"},
            {"gachí","gachís"},
            {"pirulí","pirulís"},
            {"popurrí","popurrís"},
            {"champú","champús"},
            {"menú","menús"},
            {"tutú","tutús"},
            {"vermú","vermús"},
            {"lunes","lunes"},
            {"martes","martes"},
            {"miércoles","miércoles"},
            {"jueves","jueves"},
            {"viernes","viernes"},
            {"cienpiés","cienpiés"},
            {"buscapiés","buscapiés"},
            {"pasarurés","pasapurés"},
            {"polisíndeton","polisíndeton"},
            {"trávelin","trávelin"},
            {"cátering","cátering"},
            {"hipérbaton","hipérbatos"},
            {"imán","imanes"},
            {"imam","imames"},
            {"álbum","álbumes"},
            {"sándwich","sandwiches"},
            {"paraguas","paraguas"},
            {"tijeras","tijeras"},
            {"gafas","gafas"},
            {"víveres","víveres"},
            {"virus","virus"},
            {"atlas","atlas"},
            {"déficit","déficit"},
            {"crisis","crisis"},
            {"tórax","tórax"},
            {"fórceps","fórceps"},
            {"ratio","ratios"},
            {"plus","pluses"},
            {"tos","toses"},
            {"lapsus","lapsus"},
            {"ultimátum", "ultimatos"},
            {"memorándum", "memorandos"},
            {"referéndum", "referendos"},
            {"vals","valses"}
        });

        private readonly List<ReplacementRule> PluralizationRules =
            new List<ReplacementRule>();

        private readonly List<ReplacementRule> SingularizationRules =
            new List<ReplacementRule>();

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="userDictionaryEntries"></param>
        public SpanishPluralizationService(IEnumerable<CustomPluralizationEntry> userDictionaryEntries)
            : this()
        {
            userDictionaryEntries.Each(entry =>
            {
                _userDictionaryEntries.AddValue(entry.Singular, entry.Plural);
            });
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        public SpanishPluralizationService()
        {
            //Add pluralization rules
            PluralizationRules.Add(new ReplacementRule("([aeiouáéó])$", "$1s"));
            PluralizationRules.Add(new ReplacementRule("([íú])$", "$1es"));
            PluralizationRules.Add(new ReplacementRule("([aeiou]+y$)", "$1es"));
            PluralizationRules.Add(new ReplacementRule("([b-df-hj-np-tv-z]+)y$", "$1is"));
            PluralizationRules.Add(new ReplacementRule("([a-z]+)ás$", "$1ases"));
            PluralizationRules.Add(new ReplacementRule("([a-z]+)és$", "$1eses"));
            PluralizationRules.Add(new ReplacementRule("([a-z]+)ís$", "$1ises"));
            PluralizationRules.Add(new ReplacementRule("([a-z]+)ós$", "$1oses"));
            PluralizationRules.Add(new ReplacementRule("([a-z]+)ús$", "$1uses"));
            PluralizationRules.Add(new ReplacementRule("([a-z]+)s$", "$1ses"));
            PluralizationRules.Add(new ReplacementRule("([aeiou]+)[l]$", "$1les"));
            PluralizationRules.Add(new ReplacementRule("([aeiou]+)[r]$", "$1res"));
            PluralizationRules.Add(new ReplacementRule("([aeiou]+)[n]$", "$1nes"));
            PluralizationRules.Add(new ReplacementRule("([aeiou]+)[d]$", "$1des"));
            PluralizationRules.Add(new ReplacementRule("([aeiou]+)[z]$", "$1zes"));
            PluralizationRules.Add(new ReplacementRule("([aeiou]+)[j]$", "$1jes"));
            PluralizationRules.Add(new ReplacementRule("([a-z]+)ch$", null));
            PluralizationRules.Add(new ReplacementRule("(ng|[mwckgtp])$", "$1s"));

            //Add singularization rules
            SingularizationRules.Add(new ReplacementRule("([ghñpv]e)s$", ""));
            SingularizationRules.Add(new ReplacementRule("([bcdfghjklmnñprstvwxyz]{2,}e)s$", "$1"));
            SingularizationRules.Add(new ReplacementRule("([^e])s$", "$1"));
            SingularizationRules.Add(new ReplacementRule("(oides)$", "oide"));
            SingularizationRules.Add(new ReplacementRule("(é)s$", "$1"));
            SingularizationRules.Add(new ReplacementRule("(sis|tis|xis)+$", "$1"));
            SingularizationRules.Add(new ReplacementRule("(ces)$", "z"));
            SingularizationRules.Add(new ReplacementRule("^([bcdfghjklmnñpqrstvwxyz]*)([aeiou])([ns])es$", "$1$2$3"));
            SingularizationRules.Add(new ReplacementRule("es$", ""));
        }


        /// <summary>
        /// <see cref="System.Data.Entity.Infrastructure.Pluralization.IPluralizationService"/>
        /// </summary>
        /// <param name="word"><see cref="System.Data.Entity.Infrastructure.Pluralization.IPluralizationService"/></param>
        /// <returns><see cref="System.Data.Entity.Infrastructure.Pluralization.IPluralizationService"/></returns>
        public string Pluralize(string word)
        {
            var pluralized =  Capitalize(word, InternalPluralize);

            if (!_userPluralizations.ExistsInFirst(word))
            {
                _userPluralizations.AddValue(word, pluralized);
            }

            return pluralized;
        }
        /// <summary>
        /// <see cref="System.Data.Entity.Infrastructure.Pluralization.IPluralizationService"/>
        /// </summary>
        /// <param name="word"><see cref="System.Data.Entity.Infrastructure.Pluralization.IPluralizationService"/></param>
        /// <returns><see cref="System.Data.Entity.Infrastructure.Pluralization.IPluralizationService"/></returns>
        public string Singularize(string word)
        {
            return Capitalize(word, InternalSingularize);
        }

        string InternalSingularize(string word)
        {
            if (_userDictionaryEntries.ExistsInSecond(word))
            {
                return _userDictionaryEntries.GetFirstValue(word);
            }

            if (_irregularWords.ExistsInSecond(word))
            {
                return _irregularWords.GetFirstValue(word.ToLowerInvariant());
            }

            if (_userPluralizations.ExistsInSecond(word))
            {
                return _userPluralizations.GetSecondValue(word.ToLowerInvariant());
            }

            if (IsSingular(word))
            {
                return word;
            }

            var rule = SingularizationRules.Where(ir => ir.IsValidFor(word.ToLowerInvariant()))
                                          .FirstOrDefault();
            if (rule != null)
            {
                return rule.Apply(word.ToLowerInvariant());
            }

            return word;
        }

        string InternalPluralize(string word)
        {
            DebugCheck.NotEmpty(word);

            string pluralized = word;

            if (_userDictionaryEntries.ExistsInFirst(word))
            {
                return _userDictionaryEntries.GetSecondValue(word);
            }

            if (_irregularWords.ExistsInFirst(word))
            {
                return _irregularWords.GetSecondValue(word.ToLowerInvariant());
            }

            if (_userPluralizations.ExistsInSecond(word))
            {
                return word;
            }

            var rule = PluralizationRules.Where(ir => ir.IsValidFor(word.ToLowerInvariant()))
                                         .FirstOrDefault();
            if (rule != null)
            {
                pluralized = rule.Apply(word.ToLowerInvariant());
            }

            return pluralized;
        }

        private bool IsPlural(string word)
        {
            DebugCheck.NotEmpty(word);

            if (_userDictionaryEntries.ExistsInSecond(word))
            {
                return true;
            }

            if (_irregularWords.ExistsInSecond(word))
            {
                return true;
            }

            if (_userPluralizations.ExistsInSecond(word))
            {
                return true;
            }

            //return if any singularization rule exist
            var rule = SingularizationRules.Where(ir => ir.IsValidFor(word.ToLowerInvariant()))
                                          .FirstOrDefault();

            return rule != null;
        }

        private bool IsSingular(string word)
        {
            DebugCheck.NotEmpty(word);

            if (_userDictionaryEntries.ExistsInFirst(word))
                return true;

            if (_irregularWords.ExistsInFirst(word))
                return true;

            return false;
        }

        private static bool IsCapitalized(string word)
        {
            return string.IsNullOrEmpty(word) ? false : char.IsUpper(word, 0);
        }

        private static string Capitalize(string word, Func<string, string> action)
        {
            var result = action(word);

            if (IsCapitalized(word))
            {
                if (result.Length != 0)
                {

                    var sb = new StringBuilder(result.Length);

                    sb.Append(char.ToUpperInvariant(result[0]));
                    sb.Append(result.Substring(1));

                    result = sb.ToString();
                }
            }

            return result;
        }
    }
}
