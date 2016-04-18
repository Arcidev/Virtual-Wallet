﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Blend.SampleData.WalletsSampleDataSource
{
    using System; 
    using System.ComponentModel;

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
    internal class WalletsSampleDataSource { }
#else

    public class WalletsSampleDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public WalletsSampleDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("ms-appx:/SampleData/WalletsSampleDataSource/WalletsSampleDataSource.xaml", UriKind.RelativeOrAbsolute);
                Windows.UI.Xaml.Application.LoadComponent(this, resourceUri);
            }
            catch
            {
            }
        }

        private Wallets _Wallets = new Wallets();

        public Wallets Wallets
        {
            get
            {
                return this._Wallets;
            }
        }

        private Banks _Banks = new Banks();

        public Banks Banks
        {
            get
            {
                return this._Banks;
            }
        }

        private Categories _Categories = new Categories();

        public Categories Categories
        {
            get
            {
                return this._Categories;
            }
        }
    }

    public class Wallets : System.Collections.ObjectModel.ObservableCollection<WalletsItem>
    { 
    }

    public class WalletsItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Icon _Icon = new Icon();

        public Icon Icon
        {
            get
            {
                return this._Icon;
            }

            set
            {
                if (this._Icon != value)
                {
                    this._Icon = value;
                    this.OnPropertyChanged("Icon");
                }
            }
        }

        private double _Id = 0;

        public double Id
        {
            get
            {
                return this._Id;
            }

            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        private string _Name = string.Empty;

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }

    public class Icon : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double _Id = 0;

        public double Id
        {
            get
            {
                return this._Id;
            }

            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        private string _Name = string.Empty;

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string _Path = string.Empty;

        public string Path
        {
            get
            {
                return this._Path;
            }

            set
            {
                if (this._Path != value)
                {
                    this._Path = value;
                    this.OnPropertyChanged("Path");
                }
            }
        }
    }

    public class Banks : System.Collections.ObjectModel.ObservableCollection<BanksItem>
    { 
    }

    public class BanksItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Icon1 _Icon = new Icon1();

        public Icon1 Icon
        {
            get
            {
                return this._Icon;
            }

            set
            {
                if (this._Icon != value)
                {
                    this._Icon = value;
                    this.OnPropertyChanged("Icon");
                }
            }
        }

        private double _Id = 0;

        public double Id
        {
            get
            {
                return this._Id;
            }

            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        private string _Name = string.Empty;

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }

    public class Icon1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double _Id = 0;

        public double Id
        {
            get
            {
                return this._Id;
            }

            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        private string _Name = string.Empty;

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string _Path = string.Empty;

        public string Path
        {
            get
            {
                return this._Path;
            }

            set
            {
                if (this._Path != value)
                {
                    this._Path = value;
                    this.OnPropertyChanged("Path");
                }
            }
        }
    }

    public class Categories : System.Collections.ObjectModel.ObservableCollection<CategoriesItem>
    { 
    }

    public class CategoriesItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double _Id = 0;

        public double Id
        {
            get
            {
                return this._Id;
            }

            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        private string _Name = string.Empty;

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }
#endif
}
