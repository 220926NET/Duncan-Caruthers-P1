# Project 1 Presentation

## Exposed Methods
- `POST ERS/login` - used to login a user
- `POST ERS/register` - used to register a user
- `POST ERS/ticket/submit` - used to submit a ticket
- `PUT ERS/ticket/approve/{id}` - approve ticket with given id
- `PUT ERS/ticket/deny/{id}` - deny ticket with given id
- `GET ERS/ticket/view/{user_id}` view all tickets given a user id (if manager all in system, user is all the user has submitted)
- `GET ERS/ticket/view/{user_id}/{status}` same a previous but filter by status