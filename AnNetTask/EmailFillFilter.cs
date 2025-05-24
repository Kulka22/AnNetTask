namespace AnNetTask
{
    public class Email
    {
        public string To { get; private set; }
        public string Copy { get; private set; }

        private Dictionary<string, List<string>> _addresses = new Dictionary<string, List<string>>();
        
        public Email(string to, string copy)
        {
            if (to == string.Empty || copy == string.Empty)
                throw new Exception("To и Copy должны быть заполнены!");

            CheckAddressesFormats(to);
            CheckAddressesFormats(copy);

            To = to;
            Copy = copy;

            FillAdresses(To);
            FillAdresses(Copy);
            Copy = EmailFillFilter.FilterDomains(_addresses, copy);
        }

        private void FillAdresses(string inputText)
        {
            string[] adresses = inputText.Split("; ");
            foreach (string adress in adresses)
            {
                string domain = adress.Split('@')[1];
                if (!_addresses.ContainsKey(domain))
                {
                    _addresses[domain] = new List<string>();
                    _addresses[domain].Add(adress);
                }
                else if (!_addresses[domain].Contains(adress))
                    _addresses[domain].Add(adress);
            }
        }

        private void CheckAddressesFormats(string inputText)
        {
            if (inputText.Count(x => x == ';') != (inputText.Count(x => x == '@') - 1))
                throw new Exception("Введенные адреса не соответствуют формату!");

            if (inputText.Contains("@ ") || inputText.Contains(" @"))
                throw new Exception("Введенные адреса не соответствуют формату!");

            string[] adresses = inputText.Split("; ");

            foreach (string adress in adresses)
            {
                if (!adress.Contains('@'))
                    throw new Exception("Введенные адреса не соответствуют формату!");
            }
        }
    }
    internal class EmailFillFilter
    {
        private static Dictionary<string, List<string>> _subsAddresses = InitializeSubsAdresses();
        private static Dictionary<string, List<string>> _exceptionAddresses = InitializeExceptAddresses();
        
        private static Dictionary<string, List<string>> InitializeSubsAdresses()
        {
            Dictionary<string, List<string>> tempDictionary = new Dictionary<string, List<string>>();
            tempDictionary["tbank.ru"] = new List<string>() { "t.tbankovich@tbank.ru",
            "v.veronickovna@tbank.ru"};
            tempDictionary["alfa.com"] = new List<string>() { "v.vladislavovich@alfa.com" };
            tempDictionary["vtb.ru"] = new List<string>() { "a.aleksandrov@vtb.ru" };
            
            return tempDictionary;
        }

        private static Dictionary<string, List<string>> InitializeExceptAddresses()
        {
            Dictionary<string, List<string>> tempDictionary = new Dictionary<string, List<string>>();
            tempDictionary["tbank.ru"] = new List<string>() { "i.ivanov@tbank.ru" };
            tempDictionary["alfa.com"] = new List<string>() { "s.sergeev@alfa.com",
            "a.andreev@alfa.com"};
            tempDictionary["vtb.ru"] = new List<string>();

            return tempDictionary;
        }

        public static bool CheckException(string domain, string adress)
        {
            if (!_exceptionAddresses.ContainsKey(domain))
                return false;
            return _exceptionAddresses[domain].Contains(adress);
        }

        public static string FilterDomains(Dictionary<string, List<string>> addresses, string copy)
        {
            List<string> filteredAddresses = new List<string>();
            foreach (string address in copy.Split("; "))
                filteredAddresses.Add(address);

            bool isZeroExceptAddresses = true;

            foreach (var addressesList in addresses)
            {
                string domain = addressesList.Key;
                foreach (var address in addresses[domain])
                {
                    if (CheckException(domain, address))
                    {
                        isZeroExceptAddresses = false;
                        foreach (var addressToDel in _subsAddresses[domain])
                        {
                            if (filteredAddresses.Contains(addressToDel))
                            {
                                filteredAddresses.Remove(addressToDel);
                            }
                        }
                    }  
                }
            }

            if (isZeroExceptAddresses)
            {
                foreach (string domain in addresses.Keys)
                {
                    if (!_subsAddresses.ContainsKey(domain))
                        continue;
                    foreach (string address in _subsAddresses[domain])
                    {
                        if (!filteredAddresses.Contains(address))
                            filteredAddresses.Add(address);
                    }
                }
            }

            string result = string.Empty;
            for (int i = 0; i < filteredAddresses.Count; i++)
            {
                result += filteredAddresses[i];
                if (i < (filteredAddresses.Count - 1))
                    result += "; ";
            }

            return result;
        }
    }
}