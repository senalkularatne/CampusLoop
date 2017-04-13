using System.Collections.Generic;
using System.Collections.ObjectModel;

// This is currently just filler data, but I made this to hopefully easily port to azure
// TODO: Incoorperate azure instead of filler

namespace UEApp
{
    public class EventsDataModel
    {
        public string Title { set; get; }

        public string Location { set; get; }

        public string Date { set; get; }

        public string Location_PhotoUrl { set; get; }

        public string Tag1 { set; get; }

        public string Tag2 { set; get; }

        public string Tag3 { set; get; }

        public List<string> categories = new List<string>
        {
            "Art",
            "Business",
            "Casual",
            "Food",
            "Fun",
            "Literature",
            "Math",
            "Meeting",
            "Music",
            "Science",
            "Sports",
            "Technology"
        };

        public List<string> Categories => categories;

        public static IList<EventsDataModel> All { set; get; }

        static EventsDataModel()
        {
            All = new ObservableCollection<EventsDataModel> {
                new EventsDataModel {
                    Title = "IEEE Meeting",
                    Location = "Baldwin 705",
                    Date = "Friday Jan. 25 5:00PM",
                    Location_PhotoUrl = "http://files.www.gethifi.com/snippets/google-maps/google-maps-thumbnail.jpg",
                    Tag1 = "Meeting",
                    Tag2 = "Technology",
                    Tag3 = "Science"
                },
                new EventsDataModel {
                   Title = "Meetup with lab members",
                    Location = "Cinnamon Cafe",
                    Date = "Saturday Jan. 26 3:00PM",
                    Location_PhotoUrl = "http://files.www.gethifi.com/snippets/google-maps/google-maps-thumbnail.jpg",
                    Tag1 = "Meeting",
                    Tag2 = "Science"
                },
                new EventsDataModel {
                   Title = "Bearcats Vs. Musketeers",
                    Location = "UC Basketball Stadium",
                    Date = "Saturday Jan. 26 7:15PM",
                    Location_PhotoUrl = "http://files.www.gethifi.com/snippets/google-maps/google-maps-thumbnail.jpg",
                    Tag1 = "Sports",
                    Tag2 = "Fun"
                },
                new EventsDataModel {
                    Title = "EECS Fundraiser",
                    Location = "Raising Canes",
                    Date = "Monday Jan. 28 10:00AM - 2:00PM",
                    Location_PhotoUrl = "http://files.www.gethifi.com/snippets/google-maps/google-maps-thumbnail.jpg",
                    Tag1 = "Food",
                    Tag2 = "Casual"
                }
            };
        }
    }
}