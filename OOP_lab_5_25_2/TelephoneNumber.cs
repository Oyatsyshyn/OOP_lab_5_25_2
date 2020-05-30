namespace OOP_lab_5_25_1
{
    abstract class TelephoneNumber
    {
        private string _number;
        private string _operator;

        public string Number
        {
            get => _number;
            set => _number = value;
        }

        public string Operator
        {
            get => _operator;
            set => _operator = value;
        }

        public virtual string UkrainianI(string str)
        {
            char[] ch = str.ToCharArray();

            for (int i = 0; i < ch.Length; ++i)
            {
                if (ch[i] == 'і')
                {
                    ch[i] = 'i';
                }

                if (ch[i] == 'І')
                {
                    ch[i] = 'I';
                }
            }

            return new string(ch);
        }

        public TelephoneNumber()
        {
            _number = "Не вказано";
            _operator = "Не вказано";
        }

        public TelephoneNumber(string Number, string Operator)
        {
            _number = UkrainianI(Number);
            _operator = UkrainianI(Operator);
        }
    }
}
