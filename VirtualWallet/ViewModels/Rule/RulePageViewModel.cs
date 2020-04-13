using BL.Models;
using BL.Service;
using Shared.Enums;
using System;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class RulePageViewModel : ViewModelBase
    {
        private readonly IRuleService ruleService;

        private Rule rule;
        private bool modified;
        private bool persisted;
        private bool patternMatch;
        private bool patternValid;
        private string testText;

        public RulePageViewModel(IRuleService ruleService)
        {
            this.ruleService = ruleService;
            Rule = new Rule();
            modified = false;
            patternValid = true;
        }

        public Rule Rule
        {
            get => rule;
            set
            {
                if (rule == value)
                    return;

                rule = value;
                NotifyPropertyChanged();
            }
        }

        public bool Modified
        {
            get => modified;
            set
            {
                if (modified == value)
                    return;

                modified = value;
                NotifyPropertyChanged();
            }
        }

        public bool Persisted
        {
            get => persisted;
            set
            {
                if (persisted == value)
                    return;

                persisted = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get => Rule == null ? string.Empty : Rule.Name;
            set
            {
                if (Rule == null || Rule.Name == value)
                    return;

                Rule.Name = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public string Pattern
        {
            get => Rule == null ? string.Empty : Rule.Pattern;
            set
            {
                if (Rule == null || Rule.Pattern == value)
                    return;

                Rule.Pattern = value;
                Modified = true;
                TestPattern();
                NotifyPropertyChanged();
            }
        }

        public PatternType PatternType
        {
            get => Rule == null ? PatternType.Contains : Rule.PatternType;
            set
            {
                if (Rule == null || Rule.PatternType == value)
                    return;

                Rule.PatternType = value;
                Modified = true;
                TestPattern();
                NotifyPropertyChanged();
            }
        }

        public bool PatternMatch
        {
            get => patternMatch;
            set
            {
                if (patternMatch == value)
                    return;

                patternMatch = value;
                NotifyPropertyChanged();
            }
        }

        public bool PatternValid
        {
            get => patternValid;
            set
            {
                if (patternValid == value)
                    return;

                patternValid = value;
                NotifyPropertyChanged();
            }
        }

        public string TestText
        {
            get => testText;
            set
            {
                if (testText == value)
                    return;

                testText = value;
                TestPattern();
                NotifyPropertyChanged();
            }
        }

        public int CategoryId { get; set; }

        public async Task LoadDataAsync()
        {
            var rule = await ruleService.GetAsync(Rule.Id);

            if (rule == null)
            {
                rule = new Rule
                {
                    CategoryId = CategoryId,
                    PatternType = PatternType.Contains
                };
                Persisted = false;
            }
            else
            {
                Persisted = true;
            }

            this.rule = rule;
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Pattern));
            NotifyPropertyChanged(nameof(PatternType));
            Modified = false;
        }

        public async Task SaveRuleAsync()
        {
            if (Persisted)
            {
                await ruleService.UpdateAsync(Rule);
            }
            else
            {
                await ruleService.InsertAsync(true, Rule);
            }
            Modified = false;
        }

        public async Task DiscardChangesAsync()
        {
            await LoadDataAsync();
        }

        public async Task DeleteRuleAsync()
        {
            await ruleService.DeleteAsync(Rule.Id);
        }

        private void TestPattern()
        {
            try
            {
                PatternMatch = Rule.Fits(TestText);
                PatternValid = true;
            }
            catch (ArgumentException)
            {
                PatternValid = false;
            }
        }
    }
}
