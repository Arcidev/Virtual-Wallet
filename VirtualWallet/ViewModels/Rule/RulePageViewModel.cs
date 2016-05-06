using BL.Models;
using BL.Service;
using Shared.Enums;
using System;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class RulePageViewModel : ViewModelBase
    {
        private IRuleService ruleService;

        private Rule rule;
        private Boolean modified;
        private Boolean persisted;
        private Boolean patternMatch;
        private Boolean patternValid;
        private String testText;

        public RulePageViewModel(IRuleService ruleService)
        {
            this.ruleService = ruleService;
            Rule = new Rule();
            this.modified = false;
            this.patternValid = true;
        }

        public Rule Rule
        {
            get { return rule; }
            set
            {
                if (rule == value)
                    return;

                rule = value;
                NotifyPropertyChanged();
            }
        }

        public Boolean Modified
        {
            get
            {
                return modified;
            }
            set
            {
                if (modified == value)
                    return;

                modified = value;
                NotifyPropertyChanged();
            }
        }

        public Boolean Persisted
        {
            get
            {
                return persisted;
            }
            set
            {
                if (persisted == value)
                    return;

                persisted = value;
                NotifyPropertyChanged();
            }
        }

        public String Name
        {
            get
            { 
                return Rule == null ? string.Empty : Rule.Name;
            }
            set
            {
                if (Rule == null || Rule.Name == value)
                    return;

                Rule.Name = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public String Description
        {
            get
            {
                return Rule == null ? string.Empty : Rule.Description;
            }
            set
            {
                if (Rule == null || Rule.Description == value)
                    return;

                Rule.Description = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public String Pattern
        {
            get
            {
                return Rule == null ? string.Empty : Rule.Pattern;
            }
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
            get
            {
                return Rule == null ? PatternType.Contains : Rule.PatternType;
            }
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

        public Boolean PatternMatch
        {
            get
            {
                return patternMatch;
            }
            set
            {
                if (patternMatch == value)
                    return;

                patternMatch = value;
                NotifyPropertyChanged();
            }
        }

        public Boolean PatternValid
        {
            get
            {
                return patternValid;
            }
            set
            {
                if (patternValid == value)
                    return;

                patternValid = value;
                NotifyPropertyChanged();
            }
        }

        public String TestText
        {
            get
            {
                return testText;
            }
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
                rule = new Rule();
                rule.CategoryId = CategoryId;
                rule.PatternType = PatternType.Contains;
                Persisted = false;
            } else
            {
                Persisted = true;
            }

            this.rule = rule;
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Description));
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
