using BL.Models;
using BL.Service;
using Shared.Filters;
using System;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class RulePageViewModel : ViewModelBase
    {
        private IRuleService ruleService;
        private ICategoryRuleService categoryRuleService;

        private Rule rule;
        private Boolean modified;
        private Boolean persisted;

        public RulePageViewModel(IRuleService ruleService, ICategoryRuleService categoryRuleService)
        {
            this.ruleService = ruleService;
            this.categoryRuleService = categoryRuleService;
            Rule = new Rule();
            this.modified = false;
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

        public int Id
        {
            get
            {
                return Rule == null ? -1 : Rule.Id;
            }
            set
            {
                if (Rule == null || Rule.Id == value)
                    return;

                Rule.Id = value;
                Modified = true;
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
                NotifyPropertyChanged();
            }
        }

        public bool IsRegExp
        {
            get
            {
                return Rule == null ? false : Rule.IsRegExp;
            }
            set
            {
                if (Rule == null || Rule.IsRegExp == value)
                    return;

                Rule.IsRegExp = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadDataAsync()
        {
            var ruleId = Rule.Id;
            var rule = await ruleService.GetAsync(ruleId);
            Rule = rule ?? new Rule();
            if(rule != null)
            {
                Name = rule.Name;
                Description = Rule.Description;
                Pattern = Rule.Pattern;
                IsRegExp = Rule.IsRegExp;
                Persisted = true;
            }
            Modified = false;
        }

        public async Task SaveRuleAsync()
        {
            await ruleService.InsertOrReplaceAsync(Rule);
            Modified = false;
        }

        public async Task DiscardChangesAsync()
        {
            await LoadDataAsync();
        }

        public async Task DeleteRuleAsync()
        {
            var filter = new CategoryRuleFilter(){ RuleId = Rule.Id};
            var categoriesRules = await categoryRuleService.GetAsync();

            foreach (CategoryRule catRul in categoriesRules)
            {
                await categoryRuleService.DeleteAsync(catRul.Id);
            }

            await ruleService.DeleteAsync(Rule.Id);
        }
    }
}
