FROM postgres:15

ENV POSTGRES_USER=coffeemanager
ENV POSTGRES_PASSWORD=coffeemanager
ENV POSTGRES_DB=CoffeeMachineDB

COPY initdb /docker-entrypoint-initdb.d/

EXPOSE 5432
