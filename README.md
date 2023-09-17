# E-Commerce Web Application

[Deployed website link](https://e-commerce-app.azurewebsites.net)

## Overview
This project is an e-commerce web application created with C# ASP.NET Core MVC. It's designed as an educational project
to demonstrate how to work with ASP. NET MVC.

For this project we chose to go for a tech store that sells all manner of items needed for people interested in all
manner of technical products from laptops to their needed accessories to screens

## Database schema

![Database schema](https://cdn.discordapp.com/attachments/1095054312129966161/1152665664243376138/TeckPioneers.png)

## schema explanation

We chose to work with a one-to-many relation between the categories and the products as we saw it fit that one category
would be sufficient to identify all the products that are within this category and that its difficult to label the
same product under two categories in the tech industry.

How are categories and products linked in the code you might ask?

They are linked where each product has an object of type category. And where each category has a list of objects of type product.

## Features
- **Home Page**: The landing page of the application.
- **Categories**: Users can browse products by category.
- **Products**: Users can view individual product details.
- **User Authentication**: The application includes login and signup services.

## User Roles
The application has three user roles:
1. **Administrator**: Can create new products and categories.
2. **Editor**: Can edit existing categories.
3. **User**: Can view products and categories and their details.

## Claims

- **What**: User’s ID, username, and role (admin, editor, or user).
- **Where**: These are captured during the login process.
- **Why**: To personalize the user’s experience and authorize their actions (like creating new products or editing existing categories).

## Policies

- **What**: We're enforcing a role based policy(Administrator/Editor/User) that restricts access to the (Create/ update/ delete)
actions throughout our web application
- **Why**: To ensure that only users in the “Administrator” role can create or delete new categories and products.
and only users in the "Editor" role can edit categories and products.

## Usage
Since this is a web application, it doesn't need to be installed. To use the application, navigate to the deployed URL
in your web browser.

## Contributing
- [Ahmad Harhsheh](https://github.com/AhMaD36789)
- [AlHareth alhyari](https://github.com/alharet7)

This being an educational project, contributions are welcome. Please feel free to fork the project and submit your
pull requests.