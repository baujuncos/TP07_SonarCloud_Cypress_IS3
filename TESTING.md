# Testing Guide for TikTask2.0

This guide provides instructions for testing the TikTask2.0 application.

## Manual Testing Steps

### Prerequisites
1. Both API and Web applications are running
2. API: http://localhost:5001
3. Web: http://localhost:5002

### Test Scenario 1: User Registration

1. Navigate to http://localhost:5002
2. Click on "Register here" link
3. Fill in the registration form:
   - Username: testuser
   - Email: test@example.com
   - Password: Test123!
4. Click "Register" button
5. ✅ **Expected**: Redirected to tasks page with empty task list

### Test Scenario 2: User Login

1. Click "Logout" button
2. You should be redirected to login page
3. Enter credentials:
   - Username: testuser
   - Password: Test123!
4. Click "Login" button
5. ✅ **Expected**: Redirected to tasks page

### Test Scenario 3: Create Task

1. On the tasks page, click "Add Task" button
2. Fill in the task form:
   - Title: "Complete Project Report"
   - Description: "Write and submit the final project report"
   - Due Date: Select tomorrow's date
3. Click "Save" button
4. ✅ **Expected**: Modal closes, new task appears in the list

### Test Scenario 4: Edit Task

1. Find the task you just created
2. Click "Edit" button on the task
3. Modify the task:
   - Title: "Complete and Review Project Report"
   - Description: "Write, review, and submit the final project report"
4. Click "Save" button
5. ✅ **Expected**: Task is updated with new information

### Test Scenario 5: Mark Task as Complete

1. Find any uncompleted task
2. Click "Complete" button
3. ✅ **Expected**: Task becomes slightly transparent, button changes to "Undo"
4. Click "Undo" button
5. ✅ **Expected**: Task returns to normal state

### Test Scenario 6: Delete Task

1. Find any task
2. Click "Delete" button
3. ✅ **Expected**: Task is removed from the list

### Test Scenario 7: Admin Functionality

1. Logout from current user
2. Login as admin:
   - Username: admin
   - Password: Admin123!
3. ✅ **Expected**: Notice "All Tasks" button is visible
4. Click "All Tasks" button
5. ✅ **Expected**: Can see tasks from all users with username labels
6. ✅ **Expected**: Admin cannot edit/delete other users' tasks (only view)

### Test Scenario 8: Create Multiple Tasks

1. Create at least 5 tasks with different:
   - Titles
   - Descriptions
   - Due dates (some past, some future)
2. ✅ **Expected**: All tasks display correctly in a grid layout
3. ✅ **Expected**: Tasks are ordered by creation date (newest first)

## API Endpoint Testing

You can use tools like Postman, curl, or Thunder Client (VS Code extension) to test API endpoints.

### Test API Authentication

#### Register a New User
```bash
curl -X POST http://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "apiuser",
    "email": "api@example.com",
    "password": "ApiUser123!"
  }'
```

✅ **Expected Response** (200 OK):
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "apiuser",
  "role": "User"
}
```

#### Login
```bash
curl -X POST http://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "username": "apiuser",
    "password": "ApiUser123!"
  }'
```

✅ **Expected Response** (200 OK):
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "apiuser",
  "role": "User"
}
```

Save the token for subsequent requests.

### Test Task Operations

#### Create Task
```bash
curl -X POST http://localhost:5001/api/tasks \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
    "title": "API Test Task",
    "description": "Task created via API",
    "dueDate": "2025-12-31T00:00:00Z"
  }'
```

✅ **Expected Response** (201 Created):
```json
{
  "id": 1,
  "title": "API Test Task",
  "description": "Task created via API",
  "dueDate": "2025-12-31T00:00:00Z",
  "isCompleted": false,
  "createdAt": "2025-11-05T...",
  "userId": 1
}
```

#### Get User Tasks
```bash
curl -X GET http://localhost:5001/api/tasks \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

✅ **Expected Response** (200 OK):
```json
[
  {
    "id": 1,
    "title": "API Test Task",
    "description": "Task created via API",
    "dueDate": "2025-12-31T00:00:00Z",
    "isCompleted": false,
    "createdAt": "2025-11-05T...",
    "userId": 1
  }
]
```

#### Update Task
```bash
curl -X PUT http://localhost:5001/api/tasks/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
    "title": "Updated API Test Task",
    "description": "Task updated via API",
    "dueDate": "2025-12-31T00:00:00Z"
  }'
```

✅ **Expected Response** (200 OK):
```json
{
  "id": 1,
  "title": "Updated API Test Task",
  "description": "Task updated via API",
  "dueDate": "2025-12-31T00:00:00Z",
  "isCompleted": false,
  "createdAt": "2025-11-05T...",
  "userId": 1
}
```

#### Toggle Task Completion
```bash
curl -X PATCH http://localhost:5001/api/tasks/1/complete \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

✅ **Expected Response** (200 OK):
```json
{
  "isCompleted": true
}
```

#### Delete Task
```bash
curl -X DELETE http://localhost:5001/api/tasks/1 \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

✅ **Expected Response** (204 No Content)

### Test Admin Endpoints

Login as admin first to get admin token.

#### Get All Tasks (Admin Only)
```bash
curl -X GET http://localhost:5001/api/tasks/all \
  -H "Authorization: Bearer ADMIN_TOKEN_HERE"
```

✅ **Expected Response** (200 OK):
```json
[
  {
    "id": 1,
    "title": "Task 1",
    "description": "...",
    "dueDate": "...",
    "isCompleted": false,
    "createdAt": "...",
    "userId": 1,
    "username": "testuser"
  },
  {
    "id": 2,
    "title": "Task 2",
    "description": "...",
    "dueDate": "...",
    "isCompleted": false,
    "createdAt": "...",
    "userId": 2,
    "username": "anotheruser"
  }
]
```

### Negative Test Cases

#### Test Invalid Login
```bash
curl -X POST http://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "username": "wronguser",
    "password": "wrongpass"
  }'
```

✅ **Expected Response** (401 Unauthorized):
```json
{
  "message": "Invalid username or password"
}
```

#### Test Duplicate Username
```bash
curl -X POST http://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "email": "newemail@example.com",
    "password": "Password123!"
  }'
```

✅ **Expected Response** (400 Bad Request):
```json
{
  "message": "Username already exists"
}
```

#### Test Unauthorized Access
```bash
curl -X GET http://localhost:5001/api/tasks
```

✅ **Expected Response** (401 Unauthorized)

#### Test Non-Admin Access to Admin Endpoint
```bash
curl -X GET http://localhost:5001/api/tasks/all \
  -H "Authorization: Bearer USER_TOKEN_NOT_ADMIN"
```

✅ **Expected Response** (403 Forbidden)

## Database Verification

### Check Database Content

You can inspect the SQLite database directly:

```bash
# Install sqlite3 if not available
# Ubuntu/Debian: sudo apt-get install sqlite3
# macOS: brew install sqlite3

# Open database
sqlite3 src/TikTask2.0.API/tiktask.db

# List tables
.tables

# View users
SELECT * FROM Users;

# View tasks
SELECT * FROM Tasks;

# View tasks with usernames
SELECT t.Id, t.Title, t.Description, t.DueDate, t.IsCompleted, u.Username 
FROM Tasks t 
JOIN Users u ON t.UserId = u.Id;

# Exit
.exit
```

## Performance Testing

### Load Testing with Apache Bench

```bash
# Install Apache Bench
# Ubuntu/Debian: sudo apt-get install apache2-utils
# macOS: brew install ab

# Test login endpoint (replace with actual credentials)
ab -n 100 -c 10 -p login.json -T application/json \
  http://localhost:5001/api/auth/login

# Create login.json file:
echo '{"username":"testuser","password":"Test123!"}' > login.json
```

## Automated Testing

### Unit Tests (Future Enhancement)

Create unit tests for:
- Controllers
- Services
- Authentication/Authorization
- Database operations

### Integration Tests (Future Enhancement)

Create integration tests for:
- Complete user workflows
- API endpoint combinations
- Database transactions

## Test Checklist

- [ ] User can register successfully
- [ ] User cannot register with duplicate username
- [ ] User cannot register with duplicate email
- [ ] User can login with correct credentials
- [ ] User cannot login with incorrect credentials
- [ ] User can create a task
- [ ] User can view their tasks
- [ ] User can edit their own task
- [ ] User can mark task as complete/incomplete
- [ ] User can delete their own task
- [ ] User cannot access another user's tasks
- [ ] Admin can view all tasks from all users
- [ ] Admin can see username labels on tasks
- [ ] Admin cannot edit/delete other users' tasks
- [ ] JWT token expires after 7 days
- [ ] Invalid JWT token is rejected
- [ ] API validates required fields
- [ ] UI displays validation errors
- [ ] Tasks are sorted by creation date
- [ ] Due dates are displayed correctly
- [ ] Completed tasks have visual indication
- [ ] Application handles network errors gracefully

## Known Issues and Limitations

1. **Confirmation Dialogs**: Delete confirmation uses a simple boolean check instead of a modal
2. **Offline Support**: Application requires internet connection to function
3. **File Upload**: No support for task attachments yet
4. **Email Verification**: No email verification on registration
5. **Password Reset**: No password reset functionality yet

## Reporting Issues

When reporting issues, please include:
1. Steps to reproduce
2. Expected behavior
3. Actual behavior
4. Screenshots (if applicable)
5. Browser/environment information
6. Error messages or logs

Create issues at: https://github.com/baujuncos/TP5_IS3/issues
