namespace ValidationSample.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationSample.Common;

    //using System.ComponentModel.DataAnnotations;

    /// <summary> 
    /// MainViewのViewModel 
    /// </summary> 
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        private Dictionary<string, string> Errors = new Dictionary<string, string>();

        public MainViewModel()
        {
            Value1 = 11;
            Value2 = 22;
            Value3 = 33;
            Value4 = 44;
        }

        private int _Value1;
        public int Value1
        {
            get
            {
                return this._Value1;
            }

            set
            {
                // setterで入力検証を実施する。

                if (_Value1 == value) return;

                this._Value1 = value;

                // データ検証
                if (value < 0 || value > 100)
                {
                    throw new Exception("0～100の範囲で数値を入力してください！");
                }

                RaisePropertyChanged("Value1");

            }
        }



        private int _Value2;
        public int Value2
        {
            get
            {
                return this._Value2;
            }

            set
            {
                // IDataErrorInfo を用いたチェック

                if (_Value2 == value) return;

                _Value2 = value;

                // データ検証
                Errors["Value2"] = (value < 0 || value > 100) ? "0～100の範囲で数値を入力してください！！" : null;

                RaisePropertyChanged("Value2");
                
            }
        }

        private int _Value3;
        public int Value3
        {
            get
            {
                return this._Value3;
            }

            set
            {
                this._Value3 = value;
                RaisePropertyChanged("Value3");
            }
        }

        private int _Value4;
        [CustomValidation(typeof(MainViewModel), "CustomMethod", ErrorMessage = "0～100の範囲で数値を入力してください！！！！")]
        public int Value4
        {
            get
            {
                return this._Value4;
            }

            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Value4" });
                this._Value4 = value;
                RaisePropertyChanged("Value4");
            }
        }

        public DelegateCommand CheckCommand
        {
            get
            {
                return new DelegateCommand(check);
            }
        }
        private void check()
        {
            Console.WriteLine("check");

        }


        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                return Errors.ContainsKey(columnName) ? Errors[columnName] : null;
            }
        }
        /// <summary>
        /// CustomValidation用のvalidationメソッド
        /// 本来ならvalidationクラスとして外だしにしておくとよい。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //public static bool CustomMethod(int value, ValidationContext context)　// .net4.0ならbool型を返す必要があるとの情報もあったが ValidationResultでよいっぽい？
        public static ValidationResult CustomMethod(int value, ValidationContext context)
        {

            // var obj = context.ObjectInstance as MainViewModel; // contextからビューモデルのインスタンスを取得すれば、他のプロパティの値を使った検証も可能。

            if (value < 0 || value > 100)
            {
                //return false;
                return new ValidationResult(null);
            }
            //return true;
            return ValidationResult.Success;
        }


    }

}