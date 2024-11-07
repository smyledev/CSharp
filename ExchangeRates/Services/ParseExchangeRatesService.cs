﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using WebApplication2.Model;

namespace WebApplication2.Services
{
    public class ParseExchangeRatesService
    {
        private readonly string WebPathOfExchangeRatesByPeriod =
            "https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year=";

        private readonly string WebPathOfCurrentExchangeRates =
            "https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt?date=";
        
        private List<ExchangeRate> exchangeRatesList;

        private int firstYearInt, lastYearInt, firstMonth, lastMonth, firstDay, lastDay;
        private string[] currencies;
        private int[] amountOfCurrencies;

        public List<ExchangeRate> ParseExchangeRatesByPeriod(DateTime first_date, DateTime last_date)
        {
            // Парсим и сохраняем временно переданные даты
            ParseAndSaveDates(first_date, last_date);

            // подготавливаем массивы для хранения данных
            exchangeRatesList = new List<ExchangeRate>();
            currencies = new string[35];
            amountOfCurrencies = new int[35];

            WebClient web = new WebClient();
            string downloadedString;
            string yearToParseStr;
            string[] stringsWithExchangeRates;

            int countOfYears = lastYearInt - firstYearInt;
            for (int index = 0; index <= countOfYears; index++)
            {
                yearToParseStr = (firstYearInt + index).ToString();
                downloadedString = web.DownloadString(WebPathOfExchangeRatesByPeriod + yearToParseStr);
                stringsWithExchangeRates = downloadedString.Split('\n');

                ParseAndSaveCurrencies(stringsWithExchangeRates[0]);
                ParseAndSaveExchangeRates(index, countOfYears, stringsWithExchangeRates);
            }

            return exchangeRatesList;
        }

        private void ParseAndSaveExchangeRates(int index, int countOfYears, string[] stringsWithExchangeRates)
        {
            string stringWithExchangeRates;
            string[] exchangeRates;
            string dateStr;

            // Запись всего года без проверки даты с начальной и конечной датой
            if (index > 0 && index < countOfYears)
            {
                // Обход строк по датам
                for (int j = 1; j < stringsWithExchangeRates.Length; j++)
                {
                    stringWithExchangeRates = stringsWithExchangeRates[j];
                    exchangeRates = stringWithExchangeRates.Split('|');
                    dateStr = exchangeRates[0].ToString();

                    if (dateStr != "")
                        AddExchangeRates(exchangeRates, dateStr, currencies);
                }
            }

            else
            {
                string dayStr, monthStr;
                int day, month;

                if (countOfYears > 0)
                {
                    // Сравниваем текущую дату с начальной
                    if (index == 0)
                    {
                        // Обход строк по датам
                        for (int j = 1; j < stringsWithExchangeRates.Length; j++)
                        {
                            stringWithExchangeRates = stringsWithExchangeRates[j];
                            exchangeRates = stringWithExchangeRates.Split('|');
                            dateStr = exchangeRates[0];

                            if (dateStr != "")
                            {
                                dayStr = dateStr.Substring(0, 2);
                                monthStr = dateStr.Substring(3, 2);

                                day = Int32.Parse(dayStr);
                                month = Int32.Parse(monthStr);

                                if (month >= firstMonth && day >= firstDay)
                                    AddExchangeRates(exchangeRates, dateStr, currencies);
                            }
                        }
                    }

                    // Сравниваем текущую дату с конечной
                    else if (index == countOfYears)
                    {
                        // Обход строк по датам
                        for (int j = 1; j < stringsWithExchangeRates.Length; j++)
                        {
                            stringWithExchangeRates = stringsWithExchangeRates[j];
                            exchangeRates = stringWithExchangeRates.Split('|');
                            dateStr = exchangeRates[0];

                            if (dateStr != "")
                            {
                                dayStr = dateStr.Substring(0, 2);
                                monthStr = dateStr.Substring(3, 2);

                                day = Int32.Parse(dayStr);
                                month = Int32.Parse(monthStr);

                                if (month <= lastMonth && day <= lastDay)
                                    AddExchangeRates(exchangeRates, dateStr, currencies);
                                else break;
                            }
                        }
                    }

                }


                // Сравниваем текущую дату с первой и конечной 
                else if (countOfYears == 0)
                {
                    for (int j = 1; j < stringsWithExchangeRates.Length; j++)
                    {
                        stringWithExchangeRates = stringsWithExchangeRates[j];
                        exchangeRates = stringWithExchangeRates.Split('|');
                        dateStr = exchangeRates[0];

                        if (dateStr != "")
                        {
                            dayStr = dateStr.Substring(0, 2);
                            monthStr = dateStr.Substring(3, 2);

                            day = Int32.Parse(dayStr);
                            month = Int32.Parse(monthStr);

                            if (month >= firstMonth && month <= lastMonth && day >= firstDay && day <= lastDay)
                                AddExchangeRates(exchangeRates, dateStr, currencies);
                        }
                    }
                }
            }
        }

        private void ParseAndSaveDates(DateTime first_date, DateTime last_date)
        {
            string firstDateStr = first_date.ToString("yyyy-MM-dd");
            string lastDateStr = last_date.ToString("yyyy-MM-dd");

            string firstYearStr = firstDateStr.Substring(0, 4);
            string lastYearStr = lastDateStr.Substring(0, 4);

            string firstMonthStr = firstDateStr.Substring(5, 2);
            string lastMonthStr = lastDateStr.Substring(5, 2);

            string firstDayStr = firstDateStr.Substring(8, 2);
            string lastDayStr = lastDateStr.Substring(8, 2);

            firstYearInt = Int32.Parse(firstYearStr);
            lastYearInt = Int32.Parse(lastYearStr);

            firstMonth = Int32.Parse(firstMonthStr);
            lastMonth = Int32.Parse(lastMonthStr);

            firstDay = Int32.Parse(firstDayStr);
            lastDay = Int32.Parse(lastDayStr);
        }

        private void ParseAndSaveCurrencies(string currenciesForParse)
        {
            string[] currenciesParsed = currenciesForParse.Split('|');

            string[] currenciesForSave = new string[35];
            int[] amountOfCurrenciesForSave = new int[35];

            string[] currencyAndAmount;
            if (currenciesParsed[0] == "Date")
            {
                // Обход курсов валют по строке - дате
                for (int i = 1; i < currenciesParsed.Length; i++)
                {
                    currencyAndAmount = currenciesParsed[i].Split(' ');
                    amountOfCurrenciesForSave[i - 1] = Int32.Parse(currencyAndAmount[0]);
                    currenciesForSave[i - 1] = currencyAndAmount[1];
                }
            }
            currencies = currenciesForSave;
            amountOfCurrencies = amountOfCurrenciesForSave;
        }

        //Обход курсов валют и возврат по строке - дате
        private void AddExchangeRates(string[] exchangeRatesParsed, string dateStr, string[] currencies)
        {
            string exchangeRateStr;
            double exchangeRateDouble;
            DateTime dateParsed;
            ExchangeRate exchangeRate;

            for (int index = 1; index < exchangeRatesParsed.Length; index++)
            {
                exchangeRateStr = exchangeRatesParsed[index];
                exchangeRateDouble = Double.Parse(exchangeRateStr, CultureInfo.InvariantCulture.NumberFormat);
                dateParsed = DateTime.Parse(dateStr);

                exchangeRate = new ExchangeRate()
                {
                    Id = index,
                    Date = dateParsed,
                    CurrencyCode = currencies[index - 1],
                    Rate = exchangeRateDouble / amountOfCurrencies[index - 1]
                };

                exchangeRatesList.Add(exchangeRate);
            }
        }

        public List<ExchangeRate> ParseCurrentExchangeRates()
        {
            exchangeRatesList = new List<ExchangeRate>();
            currencies = new string[35];
            amountOfCurrencies = new int[35];

            WebClient web = new WebClient();
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string downloadedString = web.DownloadString(WebPathOfCurrentExchangeRates + currentDate);
            string[] stringsWithExchangeRates = downloadedString.Split('\n');

            string stringWithExchangeRate;
            string[] exchangeRateData;
            ExchangeRate exchangeRate;
            string exchangeRateStr;
            double exchangeRateDouble;
            DateTime dateParsed;

            // Построчный обход валют 
            for (int j = 2; j < stringsWithExchangeRates.Length; j++)
            {
                stringWithExchangeRate = stringsWithExchangeRates[j];

                if (stringWithExchangeRate != "")
                {
                    exchangeRateData = stringWithExchangeRate.Split('|');

                    amountOfCurrencies[j - 2] = Int32.Parse(exchangeRateData[2]);
                    currencies[j - 2] = exchangeRateData[3];

                    exchangeRateStr = exchangeRateData[4];
                    exchangeRateDouble = Double.Parse(exchangeRateStr, CultureInfo.InvariantCulture.NumberFormat);

                    dateParsed = DateTime.Parse(currentDate);

                    exchangeRate = new ExchangeRate()
                    {
                        Id = j - 1,
                        Date = dateParsed,
                        CurrencyCode = currencies[j - 2],
                        Rate = exchangeRateDouble / amountOfCurrencies[j - 2]
                    };

                    exchangeRatesList.Add(exchangeRate);
                }
            }

            return exchangeRatesList;
        }
    }
}
