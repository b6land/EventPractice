using System;

namespace EventPractice
{
    class Program
    {
        public class News: EventArgs
        {
	        public string Title;
	        public string Content;
        }

        class Newspaper
        {
            public string Name;

            public event EventHandler LatestNews;

            public void PostNews(string content)
            {
                News news = new News() { Title = "Focus", Content = content };
                OnReceive(this, news);
            }

            protected void OnReceive(Newspaper newspaper, News news)
            {
                LatestNews?.Invoke(newspaper, news);
            }
        }


        class Subscriber
        {
            public string Name ;
            public void Notice(object sender ,EventArgs eventArgs)
            {
				Newspaper newspaper = sender as Newspaper;
                News news = eventArgs as News;
                Console.WriteLine($"I'm {Name}，Now received {news.Title}：{news.Content} from {newspaper.Name}.");
            }
        }

        // This code is rewrite from: C# 事件(下) – 加上event關鍵字 - iT 邦幫忙
        // https://ithelp.ithome.com.tw/articles/10228906
        static void Main(string[] args)
        {
            Subscriber officeWorker = new Subscriber() {Name = "Office Worker"};

            Newspaper LemonDaily = new Newspaper() {Name = "Lemon Daily"};
            Newspaper XTimes = new Newspaper() {Name = "X Times"};

            LemonDaily.LatestNews += officeWorker.Notice;
            XTimes.LatestNews += officeWorker.Notice;

            Console.WriteLine("Please input latest message: ");
            string Content = Console.ReadLine();
            LemonDaily.PostNews(Content);

            Console.WriteLine();
            Console.WriteLine("Please input latest message: ");
            Content = Console.ReadLine();
            XTimes.PostNews(Content);

            Console.ReadKey();
        }
    }
}
