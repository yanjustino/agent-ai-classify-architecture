// See https://aka.ms/new-console-template for more information

using ArchReview.AgentAI;
using ArchReview.Sintaxe;

var solutionPath = @"/Users/yanjustino/Source/org.8t4/c4sharp/C4Sharp.sln";

const string adhoc = """
                     /app/main.cs
                     /app/utils.cs
                     /app/data.txt
                     /app/config.json
                     /app/temp/temp1.tmp
                     /app/temp/temp2.tmp
                     /app/old_versions/v1/main_old.cs
                     /app/old_versions/v1/utils_old.cs
                     /app/old_versions/v2/main_v2.cs
                     /app/old_versions/v2/utils_v2.cs
                     /app/backup/backup_main.cs.bak
                     /app/backup/backup_utils.cs.bak
                     /app/misc/notes.txt
                     /app/misc/draft.docx
                     /app/misc/image.png
                     """;

const string mvc = """
                   /app/app.sln
                   /app/src/Web/Web.csproj
                   /app/src/Web/Controllers/HomeController.cs
                   /app/src/Web/Controllers/AccountController.cs
                   /app/src/Web/Controllers/ProductController.cs
                   /app/src/Web/Views/Home/Index.cshtml
                   /app/src/Web/Views/Home/About.cshtml
                   /app/src/Web/Views/Home/Contact.cshtml
                   /app/src/Web/Views/Account/Login.cshtml
                   /app/src/Web/Views/Account/Register.cshtml
                   /app/src/Web/Views/Account/Profile.cshtml
                   /app/src/Web/Views/Product/List.cshtml
                   /app/src/Web/Views/Product/Details.cshtml
                   /app/src/Web/Views/Product/Create.cshtml
                   /app/src/Web/wwwroot/css/site.css
                   /app/src/Web/wwwroot/js/site.js
                   /app/src/Web/wwwroot/lib/bootstrap/
                   /app/src/Web/wwwroot/lib/jquery/
                   /app/src/Web/appsettings.json
                   /app/src/Web/Program.cs
                   /app/src/Models/Models.csproj
                   /app/src/Models/User.cs
                   /app/src/Models/Product.cs
                   /app/src/Models/Order.cs
                   /app/src/Data/Data.csproj
                   /app/src/Data/ApplicationDbContext.cs
                   /app/src/Data/UserRepository.cs
                   /app/src/Data/ProductRepository.cs
                   /app/src/Data/OrderRepository.cs
                   """;

const string clean = """
                     /app/app.sln
                     /app/src/Application/Application.csproj
                     /app/src/Application/Interfaces/IUserService.cs
                     /app/src/Application/Interfaces/IProductService.cs
                     /app/src/Application/Services/UserService.cs
                     /app/src/Application/Services/ProductService.cs
                     /app/src/Domain/Domain.csproj
                     /app/src/Domain/Entities/User.cs
                     /app/src/Domain/Entities/Product.cs
                     /app/src/Domain/ValueObjects/Email.cs
                     /app/src/Domain/ValueObjects/Address.cs
                     /app/src/Infrastructure/Infrastructure.csproj
                     /app/src/Infrastructure/Persistence/ApplicationDbContext.cs
                     /app/src/Infrastructure/Persistence/UserRepository.cs
                     /app/src/Infrastructure/Persistence/ProductRepository.cs
                     /app/src/Infrastructure/Services/EmailService.cs
                     /app/src/Infrastructure/Services/LoggingService.cs
                     /app/src/Presentation/Presentation.csproj
                     /app/src/Presentation/Controllers/UserController.cs
                     /app/src/Presentation/Controllers/ProductController.cs
                     /app/src/Presentation/Models/UserViewModel.cs
                     /app/src/Presentation/Models/ProductViewModel.cs
                     """;

const string vertical = """
                        /app/app.sln
                        /app/src/Api/Api.csproj
                        /app/src/Api/Program.cs
                        /app/src/Api/appsettings.json
                        /app/src/Application/Application.csproj
                        /app/src/Application/Features/Users/CreateUser/CreateUserCommand.cs
                        /app/src/Application/Features/Users/CreateUser/CreateUserHandler.cs
                        /app/src/Application/Features/Users/CreateUser/CreateUserValidator.cs
                        /app/src/Application/Features/Users/CreateUser/CreateUserResponse.cs
                        /app/src/Application/Features/Users/GetUser/GetUserQuery.cs
                        /app/src/Application/Features/Users/GetUser/GetUserHandler.cs
                        /app/src/Application/Features/Users/GetUser/GetUserValidator.cs
                        /app/src/Application/Features/Users/GetUser/GetUserResponse.cs
                        /app/src/Application/Features/Products/CreateProduct/CreateProductCommand.cs
                        /app/src/Application/Features/Products/CreateProduct/CreateProductHandler.cs
                        /app/src/Application/Features/Products/CreateProduct/CreateProductValidator.cs
                        /app/src/Application/Features/Products/CreateProduct/CreateProductResponse.cs
                        /app/src/Application/Features/Products/GetProduct/GetProductQuery.cs
                        /app/src/Application/Features/Products/GetProduct/GetProductHandler.cs
                        /app/src/Application/Features/Products/GetProduct/GetProductValidator.cs
                        /app/src/Application/Features/Products/GetProduct/GetProductResponse.cs
                        """;

const string ports = """
                     /app/app.sln
                     /app/src/Application/Application.csproj
                     /app/src/Application/Ports/IOrderService.cs
                     /app/src/Application/Ports/ICustomerService.cs
                     /app/src/Application/Services/OrderService.cs
                     /app/src/Application/Services/CustomerService.cs
                     /app/src/Domain/Domain.csproj
                     /app/src/Domain/Entities/Order.cs
                     /app/src/Domain/Entities/Customer.cs
                     /app/src/Domain/ValueObjects/Address.cs
                     /app/src/Domain/ValueObjects/Email.cs
                     /app/src/Domain/Repositories/IOrderRepository.cs
                     /app/src/Domain/Repositories/ICustomerRepository.cs
                     /app/src/Infrastructure/Infrastructure.csproj
                     /app/src/Infrastructure/Adapters/Secondary/Persistence/ApplicationDbContext.cs
                     /app/src/Infrastructure/Adapters/Secondary/Persistence/OrderRepository.cs
                     /app/src/Infrastructure/Adapters/Secondary/Persistence/CustomerRepository.cs
                     /app/src/Infrastructure/Adapters/Secondary/Messaging/EmailService.cs
                     /app/src/Infrastructure/Adapters/Secondary/Messaging/SmsService.cs
                     /app/src/Presentation/Presentation.csproj
                     /app/src/Presentation/Adapters/Primary/Controllers/OrderController.cs
                     /app/src/Presentation/Adapters/Primary/Controllers/CustomerController.cs
                     /app/src/Presentation/Adapters/Primary/Models/OrderModel.cs
                     /app/src/Presentation/Adapters/Primary/Models/CustomerModel.cs
                     """;


var solution = new SolutionAnalizer();
using var agentOllama = new Agent();
var listaResult = await solution.GetFilePathFromSyntaxTrees(solutionPath);

await agentOllama.Run(listaResult);
Console.WriteLine();
await agentOllama.Run(adhoc);
Console.WriteLine();
await agentOllama.Run(mvc);
Console.WriteLine();
await agentOllama.Run(clean);
Console.WriteLine();
await agentOllama.Run(vertical);
Console.WriteLine();
await agentOllama.Run(ports);
Console.ReadLine();