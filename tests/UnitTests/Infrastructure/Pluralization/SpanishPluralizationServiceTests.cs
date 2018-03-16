namespace System.Data.Entity.Infrastructure.Pluralization
{
    using System.Collections.Generic;
    using Xunit;

    public class SpanishPluralizationServiceTests
    {
        [Fact]
        public void Singularize_user_directionary_entries_override_rules()
        {
            var pluralizationService = new SpanishPluralizationService(new List<CustomPluralizationEntry>()
            {
                new CustomPluralizationEntry(singular:"X",plural:"Y")
            });

            Assert.Equal("X", pluralizationService.Singularize("Y"));
        }

        [Fact]
        public void Pluralize_user_directionary_entries_override_rules()
        {
            var pluralizationService = new SpanishPluralizationService(new List<CustomPluralizationEntry>()
            {
                new CustomPluralizationEntry(singular:"X",plural:"Y")
            });

            Assert.Equal("Y", pluralizationService.Pluralize("X"));
        }

        [Fact]
        public void Singularize_check_irregular_word_list()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal("yóquey", pluralizationService.Singularize("yoqueis"));
        }
        [Fact]
        public void Singularize_apply_singularization_rules()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal("camion", pluralizationService.Singularize("camiones"));
            Assert.Equal("monitor", pluralizationService.Singularize("monitores"));
            Assert.Equal("pez", pluralizationService.Singularize("peces"));
            Assert.Equal("cometa", pluralizationService.Singularize("cometas"));
            Assert.Equal("producto", pluralizationService.Singularize("productos"));
            Assert.Equal("taxi", pluralizationService.Singularize("taxis"));
            Assert.Equal("buey", pluralizationService.Singularize("bueyes"));
        }
        [Fact]
        public void Singularize_return_the_sample_word_if_is_singular_in_custom_entries()
        {
            var customEntries = new CustomPluralizationEntry[]
            {
                new CustomPluralizationEntry("Plural","Plurales")
            };

            var pluralizationService = new SpanishPluralizationService(customEntries);

            Assert.Equal<string>("Plural", pluralizationService.Singularize("Plural"));
        }

        [Fact]
        public void Singularize_return_the_same_word_if_is_plural_in_irregular_list()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal<string>("Tutú", pluralizationService.Singularize("Tutú"));
        }

        [Fact]
        public void Singularize_return_the_same_word_if_is_singular()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal<string>("Cliente", pluralizationService.Singularize("Cliente"));
        }

        [Fact]
        public void Pluralize_check_irregular_word_list()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal("vermús", pluralizationService.Pluralize("vermú"));
        }
        [Fact]
        public void Singularize_return_capitalized_if_word_is_capitalized()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal("Pera", pluralizationService.Singularize("Peras"));
        }

        [Fact]
        public void Pluralize_return_capitalized_if_word_is_capitalized()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal("Peras", pluralizationService.Pluralize("Pera"));
        }

        [Fact]
        public void Pluralize_apply_pluralization_rules()
        {
            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal("planos", pluralizationService.Pluralize("plano"));
            Assert.Equal("tribus", pluralizationService.Pluralize("tribu"));
            Assert.Equal("papás", pluralizationService.Pluralize("papá"));
            Assert.Equal("sofás", pluralizationService.Pluralize("sofá"));
            Assert.Equal("bisturíes", pluralizationService.Pluralize("bisturí"));
            Assert.Equal("tisúes", pluralizationService.Pluralize("tisú"));
            Assert.Equal("bueyes", pluralizationService.Pluralize("buey"));
            Assert.Equal("convoyes", pluralizationService.Pluralize("convoy"));
            Assert.Equal("pantis", pluralizationService.Pluralize("panty"));
            Assert.Equal("dandis", pluralizationService.Pluralize("dandy"));
            Assert.Equal("toses", pluralizationService.Pluralize("tos"));
            Assert.Equal("dóciles", pluralizationService.Pluralize("dócil"));
            Assert.Equal("colores", pluralizationService.Pluralize("color"));
            Assert.Equal("panes", pluralizationService.Pluralize("pan"));
            Assert.Equal("cracs", pluralizationService.Pluralize("crac"));
            Assert.Equal("zigzags", pluralizationService.Pluralize("zigzag"));
        }

        [Fact]
        public void Pluralize_return_the_same_word_if_is_plural_in_custom_entries()
        {
            var customEntries = new CustomPluralizationEntry[]
            {
                new CustomPluralizationEntry("Plural","Plurales")
            };

            var pluralizationService = new SpanishPluralizationService(customEntries);

            Assert.Equal<string>("Plurales",pluralizationService.Pluralize("Plural"));
        }

        [Fact]
        public void Pluralize_return_the_same_word_if_is_plural_in_irregular_list()
        {

            var pluralizationService = new SpanishPluralizationService();

            Assert.Equal<string>("Albalaes", pluralizationService.Pluralize("Albalá"));
        }

        [Fact]
        public void Pluralize_check_user_pluralizations_in_pluralize()
        {
            var pluralizationService = new SpanishPluralizationService();
            pluralizationService.Pluralize("Cliente");

            Assert.Equal<string>("Clientes", pluralizationService.Pluralize("Clientes"));
        }
    }
}
