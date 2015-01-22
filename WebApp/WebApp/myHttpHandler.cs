using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class myHttpHandler: IHttpHandler
    {
        private int summ;

        private int col;

        public bool IsReusable
        {
            get { return true; }
        }
        public void ProcessRequest(HttpContext context)
        {
            string num = context.Request.QueryString["num"];

           
            if (num != null)
            { //пришел запрос вида домен/?num=
                int numericNum;

                if (int.TryParse(num, out numericNum))
                    if ((numericNum <= 10) & (numericNum > 0))
                    {//передано целое число от 1 до 10
                        summ += numericNum;
                        col++;
                        context.Response.Write("<p>Передано число = " + numericNum + "</p>");
                    }
                    else
                    {//передано целое число не пренадлежит промежутку от 1 до 10
                        context.Response.Write("<p>Переданное значение не является целым числом от 1 до 10</p>");
                    }
                else {//переданное значение не является целым числом
                        context.Response.Write("<p>Переданное значение не является целым числом</p>");
                     }
                
                context.Response.Write("<p>Сумма всех переданных чисел = " + summ + "</p>");
                context.Response.Write("<p>Количество переданных чисел = " + col + "</p>");
                context.Response.Write("<p><br>Чтобы узнать среднее значение переданных чисел,<br>");
                context.Response.Write("введите запрос вида:<br>");
                context.Response.Write("&nbsp&nbsp домен/?mean=1</p>");
            }
            else
            {
                string mean = context.Request.QueryString["mean"];

                if (mean == "1")
                {//пришел запрос вида домен/?mean=1

                    if (col > 0)
                    {//на данный момент уже передано хотябы одно значение
                        double floatMean = summ/(double)col;
                                                
                        string strf = floatMean.ToString();

                        context.Response.Write("<p>Среднее = " + strf + "</p>");
                        context.Response.Write("<p>Сумма всех значений = " + summ + "</p>");
                        context.Response.Write("<p>Кол-во переданных значений = " + col + "</p>");
                    }
                    else
                    {//на данный момент не передано ни одно значение
                        context.Response.Write("<p>Ещё не передано ни одно значение</p>");
                    }
                }
                else
                {//пришел какойто левый запрос
                    context.Response.Write("<p>Чтобы передать число на сервер,<br>");
                    context.Response.Write("введите запрос вида:<br>");
                    context.Response.Write("&nbsp&nbsp домен/?num=цифру_от_1_до_10</p>");
                    context.Response.Write("<p><br>Чтобы узнать среднее значение переданных чисел,<br>");
                    context.Response.Write("введите запрос вида:<br>");
                    context.Response.Write("&nbsp&nbsp домен/?mean=1</p>");
                }
            }
        }
    }
}