namespace System.Data.Entity.Infrastructure.Pluralization
{
    using System.Data.Entity.Utilities;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Identify a replacement rule
    /// </summary>
    internal class ReplacementRule
    {
        public string Rule { get; private set; }

        public string Replacement { get; private set; }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="rule">The rule to find</param>
        /// <param name="replacement">The replacement to use with matched rules</param>
        public ReplacementRule(string rule, string replacement)
        {
            DebugCheck.NotNull(rule);
            DebugCheck.NotEmpty(rule);


            Rule = rule;
            Replacement = replacement;
        }

        /// <summary>
        /// Check if the rule exist for the <paramref name="word"/>
        /// </summary>
        /// <param name="word">The word</param>
        /// <returns>True if rule is match in <paramref name="word"/></returns>
        public bool IsValidFor(string word)
        {
            return Regex.IsMatch(word, Rule);
        }

        /// <summary>
        /// Apply the replacement rule into <paramref name="word"/>
        /// </summary>
        /// <param name="word">The word</param>
        /// <returns>The aplied replacement</returns>
        public string Apply(string word)
        {
            if (Replacement != null)
                return new Regex(Rule).Replace(word, Replacement);
            else
                return word;
        }
    }
}
