# Athlete Hub (An Online Coaching Platform)
***
#### Athlete Hub online coaching platform connects coaches with athletes in an easy to use, elegant website.

#### 1- athlete:
- can search for coaches by any critriea 
- can sort coaches by rating, age, gender, price of subscribtions
- can subscribe with a coach on a specific bundle and pay online using stripe payment gateway
- can track his progress and add weekly performance and body measurments
- can calculate his calorie intake 
- can check all coaches ratings and other athletes comments
- can rate a coach he has subscribed with
- can add coaches to favourites list (wish-list)

  
#### 2- coach
- can add his subscribtion bundles and their details(name, price, duration, features provided)
- can view his current athletes that have active subscribtions
- can add content (videos, prictures, pdfs) to his account storage that he can use later when needed
- can view his athletes progress and measurements
- can block an athlete so he can't subscribe with him again


#### communication between website, users, and doctors
- coach recieve updates(via email) once his documents are verified and he is a verified coach on the website
- both athletes and coaches recieve an email for confirming identity and email registered on the website


#### admin Dashboard:
- admin approve coaches documents to be verified on the website
- admin can see history of the whole website
- admin can view website statistics ( number of coaches, athlete, active subscribtions, history of subscribtions)


***
## technologies and tools used :
- C#
- .Net Core Web Api
- .Net Core Identity + JWT 
- MS Sql Server
- Entity Framework Core 8 + LINQ
- Microsoft Azure blob storage
- MediatR
- Hangefire
- Serilog structured logging
- FluentValidation
***

## concepts Applied and techniques used :
- Clean Architecture + CQRS using MediatR and EF Core for command and query sides
- Using FluentValidation to seperate validation logic from controllers
- Handling file uploads using Azure Blob Storage
- Exception Handling using IExceptionHandling interface thats added in .Net 8 + custom exceptions for handling various Http error status in resposnses
- Authentication and Authorization management(role-based, resource-based) using JWT access tokens + refresh tokens
- reset password, change email, de-activate account features utilizing identity api
- facilating data conversion in serialization using Json Custom Converter


## project's front-end repo
  (https://github.com/Omnia254/AhtleteHub_Frontend)

## project's database Schema
  (https://drawsql.app/teams/tigers-14/diagrams/athletehub)

## future work
- coach can post on athlete profile (content, training and nutrition plans pdf, progress comments)
- chatting between athlete and coach (using signalR)
- replacing stripe payment with an egyptian local provider(fawry, paymob, vodafone cash)

***
### Project Team On Linked-In: 

#### Ahmed Fawzi: 
  (https://www.linkedin.com/in/ahmed-fawzi-elarabi/)

#### Mostafa Ayman:
   (https://www.linkedin.com/in/mostafa-ayman-/)
   
#### Omnya sayed:
  (https://github.com/Omnia254)

#### Mostafa Foad: 
  (https://www.linkedin.com/in/mostafa-foad/)

#### Moaz Samy:
  (https://www.linkedin.com/in/moaz-samy/)

***

 
