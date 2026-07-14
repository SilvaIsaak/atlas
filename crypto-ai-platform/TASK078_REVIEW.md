# TASK078 Review — Real Infrastructure Validation

## Summary
This task is to validate PostgreSQL/TimescaleDB, EF Core, migrations, seed data, and repositories!

## Pre-requisites
- PostgreSQL running locally or via Docker
- Connection string properly configured in `appsettings.json`
- EF Core tools installed (`dotnet tool install --global dotnet-ef`)

## Steps to Validate
1. **Create Initial Migration**
   - Run in `packages/infrastructure` folder: `dotnet ef migrations add InitialCreate --startup-project ../../apps/api-core`
2. **Apply Migration**
   - Run: `dotnet ef database update --startup-project ../../apps/api-core`
3. **Validate Seed Data**
   - Check if Demo Tenant is created
   - Check if Demo User is created (demo@cryptoai.com)
   - Check if demo Portfolio, Strategies, etc., are created
4. **Test Repository Operations**
   - Test read/write operations on Market Data, Orders, Positions
