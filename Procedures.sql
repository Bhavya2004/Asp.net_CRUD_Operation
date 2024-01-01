------------------------------Country Table--------------------------------------
--1. SelectAll from LOC_Country Table

--exec PR_Country_SelectAll
create proc PR_Country_SelectAll
as
	select * from [dbo].[LOC_Country]

--2. SelectByPK from LOC_Country Table

--exec PR_Country_SelectByPK 2
create proc PR_Country_SelectByPK
@CountryID int
as
	select * from [dbo].[LOC_Country]
	where
		[dbo].[LOC_Country].[CountryID]=@CountryID

--3. Insert In LOC_Country Table

--exec PR_Country_Insert 'U.S.A','USA'
alter proc PR_Country_Insert
@CountryName varchar(100),
@CountryCode varchar(50)
as
	insert into [dbo].[LOC_Country] ([CountryName],[CountryCode])
	values (@CountryName,@CountryCode)


--4. Update In LOC_Country Table

--exec PR_Country_Update 2 ,'U.S.A','USA'
alter proc PR_Country_Update
@CountryID int,
@CountryName varchar(100),
@CountryCode varchar(50)
as
	update LOC_Country 
	set
		[dbo].[LOC_Country].[CountryName]=@CountryName,
		[dbo].[LOC_Country].[CountryCode]=@CountryCode,
		[dbo].[LOC_Country].[Modified]= GETDATE()
	where
		[dbo].[LOC_Country].[CountryID]=@CountryID

--5. Delete form LOC_Country
 
--exec PR_Country_Delete 1
create proc PR_Country_Delete
@CountryID int
as
	delete from [dbo].[LOC_Country] 
	where 
		[dbo].[LOC_Country].[CountryID]=@CountryID

--6. Procedure for Filter Button

-- PR_Country_SelectByCountryName ''
Alter Procedure PR_Country_SelectByCountryName
@CName varchar(50)
As
Select
	CountryName
From Loc_Country
Where (@CName IS NULL OR CountryName like Concat('%',@Cname,'%'))

--7. Procedure for Country Dropdown

--PR_Country_SelectComboBox
Create Procedure PR_Country_SelectComboBox
As
Select
	CountryId,
	CountryName
From 
	LOC_Country

-------------------------State Table -------------------------------------------

--6. SelectAll from LOC_State Table

--exec PR_LOC_State_SelectAll
create proc PR_LOC_State_SelectAll
as
	select [dbo].[LOC_State].StateID,[dbo].[LOC_State].StateName,
			[dbo].[LOC_State].[StateCode], [dbo].[LOC_Country].[CountryID],
			[dbo].[LOC_Country].[CountryName],[dbo].[LOC_State].[Created],
			[dbo].[LOC_State].[Modified]
	from
		[dbo].[LOC_State] inner join [dbo].[LOC_Country]
	on
		[dbo].[LOC_State].[CountryID]=[dbo].[LOC_Country].[CountryID]

	order by 
		[dbo].[LOC_Country].[CountryName],
		[dbo].[LOC_State].[StateName]

--7. SelectByPK from LOC_State Table
create proc PR_LOC_State_SelectByPK
@StateID int
as
	select [dbo].[LOC_State].StateID,[dbo].[LOC_State].StateName,
			[dbo].[LOC_State].[StateCode], [dbo].[LOC_Country].[CountryID],
			[dbo].[LOC_Country].[CountryName],[dbo].[LOC_State].[Created],
			[dbo].[LOC_State].[Modified]
	from
		[dbo].[LOC_State] inner join [dbo].[LOC_Country]
	on
		[dbo].[LOC_State].[CountryID]=[dbo].[LOC_Country].[CountryID]
	where 
		[dbo].[LOC_State].[StateID]=@StateID

	order by 
		[dbo].[LOC_Country].[CountryName],
		[dbo].[LOC_State].[StateName]

--8 Insert into LOC_State Table

create proc PR_LOC_State_Insert
@StateName varchar(100),
@CountryID int,
@StateCode varchar(50)
as
	insert into [dbo].[LOC_State]([StateName],[CountryID],[StateCode])
	values 
		(@StateName,@CountryID,@StateCode)


--9 Update In LOC_State Table

alter proc PR_LOC_State_Update
@StateID int,
@StateName varchar(100),
@CountryID int,
@StateCode varchar(50)
as
	Update [dbo].[LOC_State] 
	set
		[dbo].[LOC_State].[StateName]=@StateName,
		[dbo].[LOC_State].[CountryID]=@CountryID,
		[dbo].[LOC_State].[StateCode]=@StateCode,
		[dbo].[LOC_State].[Modified]=GETDATE()
	where
		[dbo].[LOC_State].[StateID]=@StateID

--10. Delete from LOC_State
create proc PR_LOC_State_Delete
@StateID int
as
	delete from [dbo].[LOC_State]
	where 
		[dbo].[LOC_State].[StateID]=@StateID


------------------------------State Table-------------------------------------
--11. SelectAll from LOC_City Table

create proc PR_LOC_City_SelectAll
as
select [dbo].[LOC_City].[CityID],
 [dbo].[LOC_City].[CityName],
 [dbo].[LOC_City].[StateID],
 [dbo].[LOC_State].[StateName],
 [dbo].[LOC_Country].[CountryName],
 [dbo].[LOC_City].[Citycode],
 [dbo].[LOC_City].[CreationDate],
 [dbo].[LOC_City].[Modified]
from [dbo].[LOC_City]
inner join [dbo].[LOC_State]
on [dbo].[LOC_State].[StateID] = [dbo].[LOC_City].[StateID]
inner join [dbo].[LOC_Country]
on [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_State].[CountryID]
order by [dbo].[LOC_Country].[CountryName]
 ,[dbo].[LOC_State].[StateName]
 ,[dbo].[LOC_City].[CityName]

--12 SelectByPK from LOC_City Table

create proc PR_LOC_City_SelectByPK
@CityID int
as
select [dbo].[LOC_City].[CityID],
 [dbo].[LOC_City].[CityName],
 [dbo].[LOC_City].[StateID],
 [dbo].[LOC_State].[StateName],
 [dbo].[LOC_Country].[CountryName],
 [dbo].[LOC_City].[Citycode],
 [dbo].[LOC_City].[CreationDate],
 [dbo].[LOC_City].[Modified]
from [dbo].[LOC_City]
inner join [dbo].[LOC_State]
on [dbo].[LOC_State].[StateID] = [dbo].[LOC_City].[StateID]
inner join [dbo].[LOC_Country]
on [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_State].[CountryID]
where [dbo].[LOC_City].[CityID]=CityID
order by [dbo].[LOC_Country].[CountryName]
 ,[dbo].[LOC_State].[StateName]
 ,[dbo].[LOC_City].[CityName]

--13 Insert into LOC_City Table

create proc PR_LOC_City_Insert
@CityName varchar(100),
@StateID int,
@CountryID int,
@CityCode varchar(50)
as
insert into [dbo].[LOC_City]
(
	[CityName],
	[StateID],
	[CountryID],
	[CityCode]
)
	values
	(
	@CityName,
	@StateID,
	@CountryID,
	@CityCode
)


--14. Update In LOC_City Table

create proc PR_LOC_City_Update
@CityID int,
@CityName varchar(100),
@StateID int,
@CountryID int,
@CityCode varchar(50)
as
	update [dbo].[LOC_City]
	set [CityName] = @CityName,
	 [StateID] = @StateID,
	 [Citycode]=@CityCode,
	 [CountryID] = @CountryID,
	 [Modified]=GETDATE()
	where [dbo].[LOC_City].[CityID] = @CityID

--15 Delete from LOC_City Table

alter proc PR_LOC_City_Delete
@CityID int
as
	delete from [dbo].[LOC_City]
	where [dbo].[LOC_City].[CityID]=@CityID
