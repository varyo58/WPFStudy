namespace ViewSample.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using ViewSample.Common;
    using ViewSample;

    /// <summary> 
    /// MainViewのViewModel 
    /// </summary> 
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            SyuSyoku = FoodEnum.パン;
            Shirumono = FoodEnum.みそしる;
            Okazu = FoodEnum.ハンバーグ;

            Persons = new ObservableCollection<Person>(Person.GetPersonList());

        }

        public int count
        {
            get
            {
                return Properties.Settings.Default.ExecCount;
            }
        }

        /// <summary>
        /// viewにバインドするGenderのリスト
        /// </summary>
        public List<Gender> Genders
        {
            get
            {
                var list = new List<Gender>();
                foreach (Gender g in Enum.GetValues(typeof(Gender)))
                {
                    list.Add(g);
                }
                return list;
            }
        }
        


        private FoodEnum syuSyoku;
        public FoodEnum SyuSyoku
        {
            get
            {
                return this.syuSyoku;
            }
            set
            {
                this.syuSyoku = value;
                RaisePropertyChanged("SyuSyoku");
            }
        }
        private FoodEnum shirumono;
        public FoodEnum Shirumono
        {
            get
            {
                return this.shirumono;
            }
            set
            {
                this.shirumono = value;
                RaisePropertyChanged("Shirumono");
            }
        }
        private FoodEnum okazu;
        public FoodEnum Okazu
        {
            get
            {
                return this.okazu;
            }
            set
            {
                this.okazu = value;
                RaisePropertyChanged("Okazu");
            }
        }

        private ObservableCollection<Person> _Persons;
        public ObservableCollection<Person> Persons
        {
            get
            {
                return this._Persons;
            }
            set
            {
                this._Persons = value;
                RaisePropertyChanged("Persons");
            }
        }

        /// <summary>
        /// Testコマンド
        /// </summary>
        public DelegateCommand TestCommand
        {
            get
            {
                return new DelegateCommand(() => MessageBox.Show("Test Command."));
            }
        }


        /// <summary>
        /// ラジオボタンセット用のコマンド
        /// </summary>
        public DelegateCommand SetWaCommand
        {
            get
            {
                return new DelegateCommand(setWa);
            }
        }
        public DelegateCommand SetYouCommand
        {
            get
            {
                return new DelegateCommand(setYou);
            }
        }
        public DelegateCommand SetManpukuCommand
        {
            get
            {
                return new DelegateCommand(setManpuku);
            }
        }

        private void setWa()
        {
            SyuSyoku = FoodEnum.ごはん;
            Shirumono = FoodEnum.みそしる;
            Okazu = FoodEnum.焼き魚;
        }
        private void setYou()
        {
            SyuSyoku = FoodEnum.パン;
            Shirumono = FoodEnum.スープ;
            Okazu = FoodEnum.ハンバーグ;
        }
        private void setManpuku()
        {
            SyuSyoku = FoodEnum.カレーライス;
            Shirumono = FoodEnum.うどん;
            Okazu = FoodEnum.ステーキ;
        }

        /// <summary>
        /// 空のウィンドウを開く
        /// </summary>
        public DelegateCommand OpenWindowCommand
        {
            get
            {
                return new DelegateCommand(openWindow);
            }
        }
        private void openWindow()
        {
            Window win = new Window();
            win.Title = "new Window";
            win.Show();
        }

        /// <summary>
        /// windowの数を表示
        /// </summary>
        public DelegateCommand WindowCountCommand
        {
            get
            {
                return new DelegateCommand(() => MessageBox.Show("sub windowの数は：" + windowCount()));
            }
        }

        private int windowCount()
        {

            int cnt = 0;
            foreach(var win in Application.Current.Windows)
            {
                if(win.GetType() == typeof(Window))
//                if (win.GetType().IsSubclassOf(typeof(Window)))
                {
                    cnt++;
                }

            } 

            return cnt;
        }

    }



    /// <summary>
    /// Personクラス
    /// </summary>
    public class Person : ViewModelBase
    {

        private String _Name;
        public String Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
                RaisePropertyChanged("Name");
            }
        }

        private String _Yomi;
        public String Yomi
        {
            get
            {
                return this._Yomi;
            }
            set
            {
                this._Yomi = value;
                RaisePropertyChanged("Yomi");
            }
        }

        private int _Age;
        public int Age
        {
            get
            {
                return this._Age;
            }
            set
            {
                this._Age = value;
                RaisePropertyChanged("Age");
            }
        }

        private Boolean _Check;
        public Boolean Check
        {
            get
            {
                return this._Check;
            }
            set
            {
                this._Check = value;
                RaisePropertyChanged("Check");
            }
        }

        private Gender _Gender;
        public Gender Gender
        {
            get
            {
                return this._Gender;
            }
            set
            {
                this._Gender = value;
                RaisePropertyChanged("Gender");
            }
        }


        public static List<Person> GetPersonList()
        {
            var list = new List<Person>()
            {
                new Person() { Name = "田中", Yomi = "たなか",  Age = 29, Check = true, Gender = Gender.Women },
                new Person() { Name = "鈴木", Yomi = "すずき", Age = 33, Check = false, Gender = Gender.Women },
                new Person() { Name = "小林", Yomi = "こばやし", Age = 21, Check = false, Gender = Gender.None },
                new Person() { Name = "松井", Yomi = "まつい", Age = 33, Check = false, Gender = Gender.Men },
                new Person() { Name = "野村", Yomi = "のむら", Age = 44, Check = true, Gender = Gender.Men },
                new Person() { Name = "山田", Yomi = "やまだ", Age = 55, Check = true, Gender = Gender.None },
                new Person() { Name = "山田", Yomi = "やまだ", Age = 55, Check = true, Gender = Gender.None },
                new Person() { Name = "山田", Yomi = "やまだ", Age = 55, Check = true, Gender = Gender.None },
                new Person() { Name = "山田", Yomi = "やまだ", Age = 55, Check = true, Gender = Gender.None },
                new Person() { Name = "山田", Yomi = "やまだ", Age = 55, Check = true, Gender = Gender.None },
                new Person() { Name = "山本", Yomi = "やまもと", Age = 18, Check = false, Gender = Gender.Men },
            };
            return list;
        }


        public String NameDisp
        {
            get
            {
                return this.Name + "(" + this.Yomi + ")";
            }
        }

    }

    public enum FoodEnum
    {
        ごはん,
        パン,
        カレーライス,
        みそしる,
        スープ,
        うどん,
        ハンバーグ,
        焼き魚,
        ステーキ,
    }

    public enum Gender
    {
        None,
        Men,
        Women
    }
}