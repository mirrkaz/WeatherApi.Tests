# WeatherApi.Tests

Automated API testing framework for OpenWeatherMap Current Weather API using .NET 9, NUnit, and HttpClient.

---

## Project Structure

- **WeatherApi.Tests/** - Test project containing all test classes and utilities.  
- **TestData.cs** - Centralized test data constants.  
- **WeatherApiHelper.cs** (optional) - Utility helper for HTTP requests and environment loading.  
- **.env** - Environment file (not committed to version control) for sensitive variables like API key.

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- NUnit Test Adapter (included via NuGet)  
- [OpenWeatherMap API Key](https://openweathermap.org/appid) (free signup)

---

## Setup Instructions

1. **Clone the repository**
<pre>git clone https://github.com/mirrkaz/WeatherApi.Tests.git
cd WeatherApi.Tests</pre>


2. **Create a `.env` file**

In the `WeatherApi.Tests` project directory, create a `.env` file:
<pre>.</pre>
<pre>API_KEY=your_actual_api_key_here</pre>


3. **Add `.env` to `.gitignore`**

Ensure `.env` is added to `.gitignore` to avoid pushing secrets:

<pre>.gitignore</pre>


4. **Restore dependencies**

Run:
<pre>dotnet restore</pre>


5. **Run tests**

You can run tests via the CLI or your IDE.

- **CLI:**

  ```
  dotnet test
  ```

- **Visual Studio / Rider / VSCode**

  Use the integrated test explorer to run or debug tests.

---
## Running Tests in GitHub Actions

### Workflow Trigger

To manually trigger the tests:

1. Go to the **Actions** tab in your GitHub repository.
2. Select the workflow named `.NET Tests`
3. Click the **Run workflow** button in the top-right corner.
4. Choose the branch and click **Run workflow**.

### Viewing Test Results

After the workflow runs:

1. Go to the **Actions** tab and select the workflow run.
2. Wait till job is finished `Run WeatherApp Api Tests` then click on `WeatherApp Api Results`
2. Expand this step to see detailed test results, including which tests passed or failed.
   - Passed tests are shown with a green checkmark (✅).
   - Failed tests are shown with a red cross (❌) along with error details.

### Notes

- Test results are parsed and displayed by the [`dorny/test-reporter`](https://github.com/dorny/test-reporter) GitHub Action.
- The `.trx` test results file is generated during the test run and published to the Actions UI.
- For best results, ensure your tests generate the `.trx` file correctly, as configured in the workflow.


## Key Features

- Uses `DotNetEnv` to load environment variables from `.env` file.  
- Tests cover:  
- Valid latitude/longitude requests  
- Valid city names, including special characters (UTF-8)  
- Invalid API key scenarios  
- Invalid city names  
- Units parameter (`metric`) validation  
- Centralized test data in `TestData.cs`  
- Descriptive test cases with `[Category]` and `[Description]` attributes

---

## Notes

- Ensure your API key is valid and has sufficient quota.  
- Tests depend on live API responses; intermittent network or API issues can cause test failures.  
- Extend `TestData.cs` and test classes to cover more scenarios as needed.

---

## Useful Commands

| Command               | Description                        |
|-----------------------|----------------------------------|
| `dotnet restore`      | Restore NuGet packages           |
| `dotnet test`         | Run all tests                    |
| `dotnet add package`  | Add a NuGet package              |

---

## Troubleshooting

- **API_KEY not found or null:**  
Verify `.env` exists and contains the key `API_KEY=your_api_key`.  
Make sure the test project directory is current when running tests.

- **Tests failing with 401 Unauthorized:**  
Check if the API key is valid and not expired.

- **Special characters in city names causing issues:**  
Tests URL-encode city names before sending requests.


