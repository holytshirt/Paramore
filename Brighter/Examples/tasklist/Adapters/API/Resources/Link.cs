using Tasklist.Domain;

namespace Tasklist.Adapters.API.Resources
{
    public class Link
    {
        public Link(string relName, string href)
        {
            this.Rel = relName;
            this.HRef = href;
        }

        public Link()
        {
            //Required for serialiazation
        }

        public string Rel { get; set; }
        public string HRef { get; set; }

        public static Link Create(Task task, string hostName)
        {
            var link = new Link
                {
                    Rel = "item",
                    HRef = string.Format("http://{0}/{1}/{2}", hostName, "task", task.Id)
                };
            return link;
        }

        public static Link Create(TaskListModel taskList, string hostName)
        {
            //we don't need to use taskList to build the self link
            var self = new Link
                {
                    Rel = "self",
                    HRef = string.Format("http://{0}/{1}", hostName, "tasks")
                };

            return self;
        }

        public override string ToString()
        {
            return string.Format("<link rel=\"{0}\" href=\"{1}\" />", Rel, HRef);
        }
    }
}