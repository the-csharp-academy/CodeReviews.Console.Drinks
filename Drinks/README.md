# Drinks

**C# Console Application | REST API Consumption | HttpClient | Newtonsoft.Json | Spectre.Console**

A console-based application that consumes an external Web API (TheCocktailDB), view drinks within a category, and see full details for a specific drink — built as part of the C# Academy learning path.

Before starting this project, I completed the **"Introduction to Object-Oriented Programming"** course, and used this project as a chance to apply what it covered in a real context: enums, classes, generics (`List<T>`), constructors, properties, access modifiers, and static classes/members.

---

## Requirements

| Feature | Status |
|---|---|
| Consume an external Web API to retrieve drinks data | ✅ |
| Present a Drinks Category Menu on startup | ✅ |
| User selects a category, then a drink within it | ✅ |
| Drink detail view has no properties with empty values | ✅ |
| Handle errors so the application never crashes if the API is down | ✅ |
| **Challenge:** Favorite Drinks functionality | ✅ |
| **Challenge:** Show a picture of the drink | ✅ |
| **Challenge:** View count — track which drinks are visualized most | ✅ |
| **Challenge:** Use .NET's `HttpClient` instead of a REST client library | ✅ |

---

## How It Works

On startup, the user is shown a menu (built with an `enum`) to choose between viewing drinks, saving a favorite, or ordering a drink. From there:

- Categories are fetched from the API and shown in a table
- Choosing a category fetches and displays the drinks in it
- Choosing a drink by Id fetches its full details, filtering out any property that comes back empty, and displays only the ones with real content
- If the action chosen was "Save Favorite," the drink is added to a running favorites list (duplicates are checked and rejected)
- If the action chosen was "Order Drink," the drink's image is opened in the system's default browser
- Every drink lookup increments a view counter, tracked per drink Id

All HTTP calls, JSON deserialization, and table rendering follow the same repeating shape: send a request, check the response status, read the body, deserialize into a matching class, filter out empty values, and display the result — with `try/catch` around every API call so a failed or unreachable API shows a message instead of crashing the app.

---

## Applying the OOP Course

This was the first project where I intentionally went back to the OOP course material while building.
As first thing I followed the Youtube tutorial to understand the various parts of the code and create a base project to later develop.
Following this I studied everything that was unclear with the help of Claude to understand the code in a deeper way.
After that I tried to optimize everything with the use of enums, classes, constructors and access modifiers:

- **Enums** — `MenuActions` and `Type` replaced what would otherwise be error-prone string comparisons for menu choices and API endpoint selection
- **Classes as models** — `Category`, `Drink`, and `DrinkDetail` each describe one shape of data returned by the API, matching the JSON structure property by property
- **Constructors** — `Favorites` and view-count entries require their Id and Name to be supplied at creation, rather than allowing an incomplete object to exist
- **Static classes and members** — `DrinkService`, `FavoritesList`, and `ViewsList` are static, since each needs exactly one shared instance that persists for the whole life of the app, reachable without creating an object first


---

## Challenges

**Understanding JSON-to-class mapping** was the first real hurdle. TheCocktailDB wraps every response in an outer object with a single `"drinks"` key, meaning every endpoint needed two classes — one wrapper class matching the outer shape, and one item class matching what's inside the list. Getting comfortable with `[JsonProperty("drinks")]` (needed only when the C# property name doesn't match the JSON key exactly) and understanding that deserializing gives back one wrapper object, not a list directly, took a while to click.

**A URL-building bug** cost some debugging time early on — a missing `=` in a query string (`?c{value}` instead of `?c={value}`) meant the API silently ignored the filter parameter and returned "no data found" instead of an error I could immediately trace back to the cause.

**Static vs. non-static conflicts** came up again in this project, in a new shape: a `List<Favorites>` declared as a regular instance field inside `Program`, while `Main` (which must always stay `static`) tried to access it directly. The fix was making the list `static` as well, the same reasoning applied earlier to `MockDatabase` in a previous project — anything that needs to be reached without an instance, and persist for the app's whole lifetime, needs to be static.

**A duplicate-favorites bug** turned out to be a loop-structure mistake: the code that added a new favorite was sitting inside the same loop meant to check for existing duplicates, so it ran once per non-matching comparison instead of running once, after confirming no match existed anywhere in the list.

**Switching from a REST client library to `HttpClient`** required understanding `Task` and asynchronous calls properly for the first time. Every method ending in `Async` returns a `Task` — a "receipt" for work still in progress — and needs its own `.Result` to unwrap and block until the real value is ready. This project had two separate async operations to unwrap in sequence: sending the request (`GetAsync().Result`) and reading the response body (`ReadAsStringAsync().Result`).

**Displaying an image from a console app** meant recognizing that a terminal can't render pixels, and that "showing a picture" had to mean handing the job off to another program. An early attempt hardcoded a specific browser executable, which failed on a machine where that browser wasn't installed at that path (error that came up y following old Microsoft tutorials) — the more robust fix was `Process.Start` with `UseShellExecute = true` (Found out with Claude), letting the operating system decide which default application should open the image URL.

---

## Areas to Improve

- **A generic fetch-and-deserialize method** — I attempted to consolidate the three near-identical service methods (fetch categories, fetch drinks by category, fetch drink details) into one generic method. This ran into real friction: a method declared to return `List<T>` can't return a hardcoded `List<Category>` inside one branch and `List<Drink>` inside another, since the compiler has no way to verify `T` and the concrete type agree. I ended up keeping the three methods separate for the type-specific deserialization, but successfully extracted the shared HTTP-fetching logic (send request, get raw response) into one common method they all call into.
- **DRY logic** — As always I feel like this project could be optimized a lot more, now that I did the course I can see how far a project can actually be optimized and I feel like this is just a bad attempt at doing so, so there is still a lot of room for improvement here.
- **Access Modifiers** — I noticed I still struggle with identifying the best access modifier to apply for a certain class and when to use the static keyword.

---

## What I Learned

- Making HTTP requests to a third-party API and reading the response
- Mapping nested JSON structures to C# classes, including when property names need explicit translation via `[JsonProperty]`
- Applying enums, generics, constructors, and static members from the OOP course in a practical, non-trivial project
- Using reflection (`PropertyInfo`, `GetProperties()`, `GetValue()`) to build a display table dynamically, without hardcoding column names per type
- The difference between a `Task` and its unwrapped result, and why every `Async` method call needs its own `.Result`
- Migrating from a third-party HTTP library to .NET's built-in `HttpClient`, and why a single shared `HttpClient` instance should be reused rather than creating a new one per request
- Using `Process.Start` to hand off work to another application, and why `UseShellExecute = true` is more portable than hardcoding a specific program's executable name
- Debugging loop-ordering bugs, where an action placed inside the wrong part of a loop runs more or fewer times than intended
