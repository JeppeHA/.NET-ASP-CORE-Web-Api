CREATE TABLE 
    IF NOT EXISTS products(
        id UUID PRIMARY KEY,
        name TEXT NOT NULL,
        category TEXT NOT NULL,
        sub_category TEXT NOT NULL
    );