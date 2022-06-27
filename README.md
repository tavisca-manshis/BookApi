# Book Api

## Methods

### Get All books
```GET /book```

### Get Book by ID
```GET /book/getBook/{id}```

### Get Books by Author and Genre
```GET /book/getBooks/{author}/{genre}```

### Add Book
```GET /book/addBook```

##### Body: 

```
{
"name": "It",
"author": "Stephen King",
"genre": "Horror"
}
```
### Edit Book 
```GET /book/editBook/{id}```
##### Body: 

```
{
"id": 1
"name": "It",
"author": "Stephen King",
"genre": "Horror"
}
```
### Delete Book
```GET /book/deleteBook/{id}```
