# API Client Generation Tools 

|         | Visual Studio           | Visual Studio Code  | JetBrains Rider  |
| ------------- |:-------------:| -----:|-----:|
| Status     | [released v0.0.2](https://marketplace.visualstudio.com/items?itemName=dmitry-pavlov.ApiClientGenerationTools)  | exploring extensibility options  | exploring extensibility options |

## For Visual Studio

[API Client Generation Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=dmitry-pavlov.ApiClientGenerationTools) is a Visual Studio extension to generate `C#` and `TypeScript` `HttpClient` code for `OpenAPI` (formerly [`Swagger API`](https://swagger.io/docs/specification/about/)) web service with [NSwag](https://github.com/RSuter/NSwag) toolchain.

Simply put, it does the same as `Add Service Reference` for `WCF` or `Add Web Reference` - for `WSDL`, but now it is for `JSON` `API`, you can put it anywhere in your project and it can generate `C#` amd `TypeScript`.

# Getting Started

Install from `Tools -> Extensions and Updates` menu inside Visual Studio or [download](https://marketplace.visualstudio.com/items?itemName=dmitry-pavlov.ApiClientGenerationTools)  as `VSIX` package from Visual Studio Marketplace

# Tips & Tricks:
- To get started - from `Solution Explorer` -> `Add` -> `New Item...` -> `PetStore.nswag` to your `C#` project.
- Use [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio) - a Windows desktop app for configuring .nswag settings visually.
- Configure Visual Studio to automatically open .nswag files in NSwagStudio: in right click on .nswag file in Solution Explorer -> Open With... -> Add -> extension to [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio) app.
- To regenerate code quickly just select .nswag file and press CTRL+S.
- If code is not generated check .nswag file `Custom Tool` in Property Window. There is should be `NswagCodeGenerator`. You can just add it manually or select `Generate API Client` in context menu.

# HowTos
- [ ] [Request How-To](https://github.com/dmitry-pavlov/api-client-generation-tools/issues/new?title=DOC) you need.

# Roadmap
See the [changelog](docs/vs/CHANGELOG.MD) for the further development plans and version history.

# Feedback
Please feel free to [request a feature](https://github.com/dmitry-pavlov/api-client-generation-tools/issues/new?title=FEATURE) or [report a bug](https://github.com/dmitry-pavlov/api-client-generation-tools/issues/new?title=BUG).

# Support Development
Note a lovely :heart: `Sponsor` button with available options at the top of this page. Your support is much appreciated!
