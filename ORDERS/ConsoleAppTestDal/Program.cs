﻿using DAL;
using Entities.Models;
using System.Linq.Expressions;
using System.Reflection;

//CreateAsync().GetAwaiter().GetResult();
//RetreieveAsync().GetAwaiter().GetResult();
//UpdateAsync().GetAwaiter().GetResult();
//Filterasync().GetAwaiter().GetResult();
Deleteasync().GetAwaiter().GetResult();

Console.ReadKey();
static async Task CreateAsync()
{
    //Add Customer
    Customer customer = new Customer()
    {
        FirstName = "Miguel",
        LastName = "Gomez",
        City = "Bogotá",
        Country = "Colombia",
        Phone = "3125594627"

    };

    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            var createdCustomer = await repository.CreateAsync(customer);
            Console.WriteLine($"Added Customer: {createdCustomer.FirstName} {createdCustomer.LastName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error {ex.Message}");
        }


    }

}

static async Task RetreieveAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            Expression<Func<Customer, bool>> criteria = c => c.FirstName == "Miguel" && c.LastName == "Gomez";
            var customer = await repository.RetreiveAsync(criteria);
            if (customer != null)
            {
                Console.WriteLine($"Retrived customer: {customer.FirstName} \t{customer.LastName}\t City: {customer.City}\t Country: {customer.Country}");
            }
            Console.WriteLine($"Customer not exist");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

static async Task UpdateAsync()
{
    //Ya existe el objeto a modificar


    using (var repository = RepositoryFactory.CreateRepository())
    {
        var customerToUpdate = await repository.RetreiveAsync<Customer>(c => c.Id == 52);

        if (customerToUpdate != null)
        {
            customerToUpdate.FirstName = "Alexander";
            customerToUpdate.LastName = "Feuer";
            customerToUpdate.City = "Madrid";
            customerToUpdate.Country = "Spain";
            customerToUpdate.Phone = "(91) 666 85 82";
        }

        try
        {
            bool updated = await repository.UpdateAsync(customerToUpdate);
            if (updated)
            {
                Console.WriteLine("Customer updated succesfully.");
            }
            else
            {
                Console.WriteLine("Customer Updated failed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

static async Task Filterasync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        Expression<Func<Customer, bool >> criteria = c => c.Country == "USA";

        var customers = await repository.FilterAsync(criteria);

        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer:  { customer.FirstName} {customer.LastName} \t from {customer.City}");
        }

    }
}

static async Task Deleteasync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        Expression<Func<Customer, bool>> criteria = customer => customer.Id == 93;

        var customerToDelete = await repository.RetreiveAsync(criteria);
        if (customerToDelete != null)
        {
            bool deleted = await repository.DeleteAsync(customerToDelete);
            Console.WriteLine(deleted ? "Customer deleted succesfully. " : "Failed to delete customer.");
        }
    }
}