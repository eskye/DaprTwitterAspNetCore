namespace TwitterMontior
{
    using System.Collections.Generic;

    public class Tweet
    {
        public object coordinates { get; set; }
        public string created_at { get; set; }
        public object current_user_retweet { get; set; }
        public dynamic entities { get; set; }
        public int favorite_count { get; set; }
        public bool favorited { get; set; }
        public string filter_level { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public long in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string lang { get; set; }
        public bool possibly_sensitive { get; set; }
        public int quote_count { get; set; }
        public int reply_count { get; set; }
        public int retweet_count { get; set; }
        public bool retweeted { get; set; }
        public object retweeted_status { get; set; }
        public string source { get; set; }
        public object scopes { get; set; }
        public string text { get; set; }
        public string full_text { get; set; }
        public IList<int> display_text_range { get; set; }
        public object place { get; set; }
        public bool truncated { get; set; }
        public object user { get; set; }
        public bool withheld_copyright { get; set; }
        public object withheld_in_countries { get; set; }
        public string withheld_scope { get; set; }
    }
}
