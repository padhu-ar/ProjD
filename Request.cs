
    public class Request
    {
        public int RequestID { get; set; }
        public string RequesterName { get; set; }
        public DateTime RequestDate { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
        public string RequestStatus { get; set; }

        public Request(int requestID, string requesterName, DateTime requestDate, string description, string resolution, string requestStatus)
        {
            RequestID = requestID;
            RequesterName = requesterName;
            RequestDate = requestDate;
            Description = description;
            Resolution = resolution;
            RequestStatus = requestStatus;
        }
    }

    public class InvalidRequestStatusException : Exception
    {
        public InvalidRequestStatusException(string message) : base(message)
        {

        }
    }

    public class RequestUtility
    {
        private static readonly Random random = new Random();
        private static List<Request> requests = new List<Request>();

        public bool MakeRequest(Request request)
        {
            if (request == null)
                return false;

            request.RequestID = random.Next(1000, 10001);

            if (request.RequestStatus != "Open" && request.RequestStatus != "Close")
                throw new InvalidRequestStatusException("Request status must be either 'Open' or 'Close'.");

            requests.Add(request);
            return true;
        }

        public bool UpdateRequestStatus(int requestID, string resolution, string requestStatus)
        {
            Request request = RequestDetails(requestID);
            if (request == null)
                return false;

            if (requestStatus != "Open" && requestStatus != "Close")
                throw new InvalidRequestStatusException("Request status must be either 'Open' or 'Close'.");

            request.Resolution = resolution;
            request.RequestStatus = requestStatus;
            return true;
        }

        public Request RequestDetails(int requestID)
        {
            foreach (var req in requests)
            {
                if (req.RequestID == requestID)
                    return req;
            }
            return null;
        }

        public List<Request> OpenRequests()
        {
            List<Request> openRequests = new List<Request>();
            foreach (var req in requests)
            {
                if (req.RequestStatus == "Open")
                    openRequests.Add(req);
            }
            return openRequests.Count > 0 ? openRequests : null;
        }
    }
