<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Vertical Slice Architecture Demo Project</title>
  <style>
    body {
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
      margin: 2rem;
      line-height: 1.6;
    }
    h1, h2, h3 {
      color: #2c3e50;
    }
    code {
      background-color: #f4f4f4;
      padding: 2px 4px;
      border-radius: 3px;
    }
    pre {
      background: #f9f9f9;
      border-left: 4px solid #3498db;
      padding: 10px;
      overflow-x: auto;
    }
    a {
      color: #2980b9;
      text-decoration: none;
    }
    a:hover {
      text-decoration: underline;
    }
  </style>
</head>
<body>

<h1>Vertical Slice Architecture Demo Project</h1>

<p>Welcome to the <strong>Vertical Slice Architecture (VSA) Demo Project</strong> — a hands-on example demonstrating clean, modular backend design using CQRS, Minimal APIs, and unit testing in .NET.</p>

<h2>📂 Table of Contents</h2>
<ul>
  <li><a href="#overview">Overview</a></li>
  <li><a href="#architecture--design">Architecture & Design</a></li>
  <li><a href="#project-structure">Project Structure</a></li>
  <li><a href="#features-implemented">Features Implemented</a></li>
  <li><a href="#setup--run">Setup & Run</a></li>
  <li><a href="#testing">Testing</a></li>
  <li><a href="#technologies-used">Technologies Used</a></li>
  <li><a href="#how-to-contribute">How to Contribute</a></li>
  <li><a href="#resources">Resources</a></li>
  <li><a href="#contact">Contact</a></li>
</ul>

<h2 id="overview">📘 Overview</h2>
<p>This project is a learning-focused demo aimed to explore <strong>Vertical Slice Architecture (VSA)</strong> in .NET with the following goals:</p>
<ul>
  <li>Organize code by feature slices rather than technical layers</li>
  <li>Implement <strong>CQRS</strong> with a custom <strong>Dispatcher</strong></li>
  <li>Use <strong>Minimal APIs</strong> for lightweight modular routing</li>
  <li>Write isolated <strong>unit tests</strong> using In-Memory DB</li>
</ul>

<h2 id="architecture--design">🏗️ Architecture & Design</h2>
<ul>
  <li><strong>Vertical Slice Architecture:</strong> Each feature has its own folder with handlers, DTOs, endpoints, etc.</li>
  <li><strong>CQRS:</strong> Separates write (commands) and read (queries) operations.</li>
  <li><strong>Dispatcher:</strong> Custom dispatcher to invoke commands/queries without MediatR.</li>
  <li><strong>Minimal APIs:</strong> Simpler, lightweight API route definition.</li>
  <li><strong>Unit Testing:</strong> All features are independently testable.</li>
</ul>

<h2 id="project-structure">🧱 Project Structure</h2>
<pre>
/VerticalSliceDemo
|-- /Domain               # Entities
|-- /Infrastructure       # Persistence (DbContext, Repos)
|-- /Presentation         # Minimal APIs & Feature Slices
|    |-- /Features        # Product CRUD Features
|-- /Tests                # xUnit Test Projects
</pre>

<h2 id="features-implemented">✅ Features Implemented</h2>
<ul>
  <li>Create Product</li>
  <li>Get All Products</li>
  <li>Get Product By Id</li>
  <li>Update Product</li>
  <li>Remove Product</li>
</ul>

<h2 id="setup--run">⚙️ Setup & Run</h2>

<ol>
  <li><strong>Clone the repo:</strong>
    <pre>git clone https://github.com/yourusername/VerticalSliceDemo.git</pre>
  </li>
  <li><strong>Open in Visual Studio 2022 / VS Code</strong></li>
  <li><strong>Restore packages:</strong>
    <pre>dotnet restore</pre>
  </li>
  <li><strong>Run the API:</strong>
    <pre>dotnet run --project Presentation</pre>
  </li>
  <li><strong>Open Swagger UI:</strong> <code>https://localhost:{port}/swagger</code></li>
</ol>

<h2 id="testing">🧪 Testing</h2>
<ol>
  <li>Navigate to the test project folder</li>
  <li>Run tests using:
    <pre>dotnet test</pre>
  </li>
</ol>
<p>Tests use <code>EF Core In-Memory</code> database. Each feature handler is covered.</p>

<h2 id="technologies-used">🛠 Technologies Used</h2>
<ul>
  <li>.NET 7 / .NET 8</li>
  <li>Minimal APIs</li>
  <li>Entity Framework Core</li>
  <li>FluentValidation</li>
  <li>xUnit</li>
</ul>

<h2 id="how-to-contribute">🤝 How to Contribute</h2>
<ol>
  <li>Fork the repo</li>
  <li>Create a feature branch</li>
  <li>Commit and push</li>
  <li>Submit a pull request</li>
</ol>

<h2 id="resources">📚 Resources</h2>
<ul>
  <li><a href="https://jasontaylordev.medium.com/vertical-slice-architecture-in-asp-net-core-ef-core-ddd-1e4125eb7ef8">Vertical Slice Architecture Guide</a></li>
  <li><a href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis">Minimal APIs Docs</a></li>
  <li><a href="https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs">CQRS Pattern</a></li>
  <li><a href="https://docs.fluentvalidation.net/en/latest/">FluentValidation</a></li>
</ul>

<h2 id="contact">📨 Contact</h2>
<p>Built by <strong>Mr. Aaban</strong> – Passionate about Software Engineering and Clean Architecture.</p>

</body>
</html>
