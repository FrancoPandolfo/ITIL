namespace ITIL.Data
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get the userId from the request headers
            string userId = context.Request.Headers["user-id"].ToString();

            // Add the userId to the response headers
            if (!string.IsNullOrEmpty(userId))
            {
                context.Response.Headers.Add("user-id", userId);
            }

            await _next(context);
        }


        private string GetUserIdFromSource(HttpContext context)
        {
            // Retrieve the user ID from the desired source
            // For example, if you want to retrieve it from the headers:
            string userId = context.Request.Headers["userId"].ToString();

            // You can also retrieve it from other sources such as cookies, query parameters, etc.
            // Adjust the code accordingly based on your specific requirements.

            return userId;
        }
    }
}
