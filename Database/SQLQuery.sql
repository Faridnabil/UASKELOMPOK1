/* 
* TABEL JENIS SURAT
*/

/* view data */
CREATE PROCEDURE viewJenis
AS
select * from tbl_jenisSurat
GO
EXEC viewJenis

/* Mencari data */
CREATE PROCEDURE cariJenis @kode varchar(50)
AS
	SELECT *From tbl_jenisSurat
	WHERE kodeSurat like '%' + @kode + '%'
GO
EXEC cariJenis 'SU'

/*Input data */
CREATE PROC inputData @kode varchar(50),
					@keterangan varchar(50)
As 
	INSERT INTO tbl_jenisSurat(kodeSurat, keterangan)
	VALUES(@kode, @keterangan)
GO
EXEC inputData 'nana','nunu'

/*update data */
CREATE PROCEDURE editJenisSurat @id varchar(50),
					@kode varchar(50),
					@keterangan varchar(50)
AS
UPDATE tbl_jenisSurat SET kodeSurat=@kode, keterangan=@keterangan
WHERE id= @id
GO
EXEC editJenisSurat 

/*Hapus data */
CREATE PROCEDURE hapusJenisSurat @id AS varchar(30)
AS
DELETE FROM tbl_jenisSurat
WHERE id = @id
GO
EXEC hapusJenisSurat


/* 
* TABEL USER
*/

/* view data */
CREATE PROCEDURE viewUser
AS
select * from tbl_user
GO
EXEC viewUser

/* Mencari data */
CREATE PROCEDURE cariUser @name varchar(50)
AS
	SELECT *From tbl_user
	WHERE name like '%' + @name + '%'
GO
EXEC cariUser 'vira'

/*Input data */
CREATE PROC inputUser @name varchar(50),
					@email varchar(50),
					@password varchar(50),
					@level varchar(50)
As 
	INSERT INTO tbl_user(name, email, password, levelU)
	VALUES(@name, @email, @password, @level)
GO
EXEC inputUser 'nana','nunu','nini','user'

/*update data */
CREATE PROCEDURE updateUser @id varchar(50),
					@name varchar(50),
					@email varchar(50),
					@password varchar(50),
					@level varchar(50)
AS
UPDATE tbl_user SET name=@name, email=@email, password=@password, levelU=@level
WHERE id= @id
GO
EXEC updateUser

/*Hapus data */
CREATE PROCEDURE hapusUser @id AS varchar(30)
AS
DELETE FROM tbl_user
WHERE id= @id
GO
EXEC hapusUser '5'





/* 
* TABEL Surat Masuk
*/

/* view data */
CREATE PROCEDURE viewMasuk
AS
select * from tbl_suratMasuk
GO
EXEC viewMasuk

/* Mencari data */
CREATE PROCEDURE cariMasuk @nomor varchar(50)
AS
	SELECT *From tbl_suratMasuk
	WHERE noSmasuk like '%' + @nomor + '%'
GO
EXEC cariMasuk'Naruto'

/*Input data */
CREATE PROC inputMasuk @nomor varchar(50),
					@tanggal varchar(50),
					@pengirim varchar(50),
					@perihal varchar(50)
As 
	INSERT INTO tbl_suratMasuk(noSmasuk,tglMasuk,pengirim,perihal)
	VALUES(@nomor,@tanggal,@pengirim,@perihal)
GO
EXEC inputMasuk  'nana','2021/03/01','nini','user'

/*update data */
CREATE PROCEDURE updateMasuk @id varchar(50),
					@nomor varchar(50),
					@tanggal varchar(50),
					@pengirim varchar(50),
					@perihal varchar(50)
AS
UPDATE tbl_suratMasuk SET noSmasuk=@nomor, tglMasuk=@tanggal, pengirim=@pengirim, perihal=@perihal
WHERE id= @id
GO
EXEC updateMasuk 

/*Hapus data */
CREATE PROCEDURE hapusMasuk @id AS varchar(30)
AS
DELETE FROM tbl_suratMasuk
WHERE id = @id
GO
EXEC hapusMasuk





/* 
* TABEL Surat keluar
*/

/* view data */
CREATE PROCEDURE viewKeluar
AS
select * from tbl_suratKeluar
GO
EXEC viewKeluar

/* Mencari data */
CREATE PROCEDURE cariKeluar @nomor varchar(50)
AS
	SELECT *From tbl_suratKeluar
	WHERE noSkeluar like '%' + @nomor + '%'
GO
EXEC cariKeluar'1'

/*Input data */
CREATE PROC inputKeluar @nomor varchar(50),
					@tanggal varchar(50),
					@tujuan varchar(50),
					@perihal varchar(50)
As 
	INSERT INTO tbl_suratKeluar(noSkeluar,tglKeluar,tujuan,perihal)
	VALUES(@nomor,@tanggal,@tujuan,@perihal)
GO
EXEC inputKeluar  'nanaw','2021/03/01','nini','user'

/*update data */
CREATE PROCEDURE updateKeluar @id varchar(50),
					@nomor varchar(50),
					@tanggal varchar(50),
					@tujuan varchar(50),
					@perihal varchar(50)
AS
UPDATE tbl_suratKeluar SET noSkeluar=@nomor, tglKeluar=@tanggal, tujuan=@tujuan, perihal=@perihal
WHERE id= @id
GO
EXEC updateKeluar 

/*Hapus data */
CREATE PROCEDURE hapusKeluar @id AS varchar(30)
AS
DELETE FROM tbl_suratKeluar
WHERE id = @id
GO
EXEC hapusKeluar 

select * from tbl_user