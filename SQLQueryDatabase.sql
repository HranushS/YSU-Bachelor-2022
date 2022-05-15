use MyDb

CREATE TABLE products (
	product_id INT IDENTITY (1, 1) PRIMARY KEY,
	product_name VARCHAR (255) NOT NULL,
	brand VARCHAR(50), 
	category VARCHAR(50), 
	production_date date,
	expiry_date date,
	weight float(50),
	size float(50),
	img varbinary(MAX),
	detailed_description text


);

CREATE TABLE stores (
	store_id INT IDENTITY (1, 1) PRIMARY KEY,
	store_name VARCHAR (255) NOT NULL,
	phone VARCHAR (25),
	email VARCHAR (255),
	address VARCHAR (255),
	free_space float(50),
	min_temperature int,
	max_temperature int,
	relative_humidity int,
	zip_code VARCHAR (5)
	
);
CREATE TABLE staffs (
	staff_id INT IDENTITY (1, 1) PRIMARY KEY,
	first_name VARCHAR (50) NOT NULL,
	last_name VARCHAR (50) NOT NULL,
	full_name as first_name+' '+last_name,
	email VARCHAR (100) NOT NULL UNIQUE,
	phone VARCHAR (25) UNIQUE,
	img varbinary(MAX),
	position VARCHAR (25) NOT NULL CHECK (position IN('Manager', 
	'CEO','COO','CFO','CMO','CTO',
	'President',
	'Porter',
	'Watcher',
	'Cleaner')) DEFAULT 'Manager',
	start_date date DEFAULT GETDATE(),
	end_date date,
	store_id int Not null,
	FOREIGN KEY (store_id) 
        REFERENCES stores (store_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	

);

CREATE TABLE customers (
	customer_id INT IDENTITY (1, 1) PRIMARY KEY,
	first_name VARCHAR (255) NOT NULL,
	last_name VARCHAR (255) NOT NULL,
	full_name as first_name+' '+last_name,
	phone VARCHAR (25),
	email VARCHAR (255) NOT NULL,
	address VARCHAR (255),
	
);

CREATE TABLE orders (
	order_id INT IDENTITY (1, 1) PRIMARY KEY,
	customer_id INT,
	order_status VARCHAR(25) NOT NULL CHECK (order_status in('Pending','Processing','Rejected','Completed')) DEFAULT 'Processing',
	-- Order status: 1 = Pending; 2 = Processing; 3 = Rejected; 4 = Completed
	order_date DATE NOT NULL DEFAULT GETDATE(),
	required_date DATE NOT NULL DEFAULT GETDATE(),
	shipped_date DATE NOT NULL DEFAULT GETDATE(),
	store_id INT NOT NULL,
	FOREIGN KEY (customer_id) 
        REFERENCES customers (customer_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (store_id) 
        REFERENCES stores (store_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	
);

CREATE TABLE order_items(
	order_id INT,
	item_id INT,
	product_id INT NOT NULL,
	quantity INT NOT NULL,
	PRIMARY KEY (order_id, item_id),
	FOREIGN KEY (order_id) 
        REFERENCES orders (order_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (product_id) 
        REFERENCES products (product_id) 
        ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE stocks (
	store_id INT,
	product_id INT,
	quantity INT,
	PRIMARY KEY (store_id, product_id),
	FOREIGN KEY (store_id) 
        REFERENCES stores (store_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (product_id) 
        REFERENCES products (product_id) 
        ON DELETE CASCADE ON UPDATE CASCADE
);
ALTER TABLE staffs
ALTER COLUMN position varchar(50);

use AppDb
ALTER TABLE staffs
alter column position varchar(50) NOT NULL CHECK (position IN ('Store Manager', 'Chief Executive Officer', 'CEO',
	'COO',
	'CFO','CMO','CTO',
	'President',
	'Manager',
	'Porter',
	'Watcher',
	'Cleaner')) ;

	use MyDb
	alter table orders
	add type VARCHAR(25) NOT NULL CHECK (type in('Entry', 'Exit')) DEFAULT 'Entry';

	use MyDb
	alter table orders
	alter column store_id INT ;

	use MyDb
	alter table orders
	add product_id INT,
	quantity INT,
	FOREIGN KEY (product_id) 
        REFERENCES products (product_id) 
        ON DELETE CASCADE ON UPDATE CASCADE;

	drop table order_items

	alter table orders
	alter column quantity INT NOT NULL

	use MyDb
	CREATE TABLE users (
	user_id INT IDENTITY (1, 1) PRIMARY KEY,
	FirstName VARCHAR (25) NOT NULL,
	Phone VARCHAR (25) Not Null Unique,
	LastName VARCHAR (25) not null,
	Password VARCHAR (25) not null,
	CreatedOn datetime not null default GetDate());