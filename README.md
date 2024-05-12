 Commands:
 //may have to install this one
dotnet tool install --global dotnet-ef 

dotnet add package Microsoft.EntityFrameworkCore -v 7.0.5

dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 7.0.3

dotnet add package Microsoft.EntityFrameworkCore.Tools -v 7.0.3

dotnet add package Microsoft.EntityFrameworkCore.Design -v 7.0.3


dotnet ef database drop


dotnet ef migrations add init
dotnet ef database update

<!-- ------------------------------------------------------------------------ -->

{
  "username": "AshurN",
  "password": "password123"

  "username": "Leonardo",
  "password": "password123"
}

Db credentials
Server:
localinezation.database.windows.net

username:
LocaLinezation

password:
ALZ3426#$@^


Requirements:
    pages:
        - profilePage
            - add blog
            -edit blog
            -delete blog

    controllers / folder
        - UserController / file
            - Create user / endpoint | C
            - Login user  / endpoint | R
            - Update user / endpoint | U
            - Delete user / endpoint | D
        - BlogController / file
            - Create Blog Item / endpoint
            - Delete Blog Item / endpoint
            - Update Blog Item / endpoint
            - Get Blog

            

    Services / folder
        - contex/ folder
            - DataContext /file
        
        - UserService /file
            - create user / functions | C
            - Login user / funtions   | R
            - Update user /functions  | U
            - Delete user / functions | D
            - GetUserByUsername / function
        
        - BlogServices - file
        - PasswordService /file
            -Hash and Salt

    Models /folder
        - UserModel / file
             int ID
             string Username
             string Salt
             String Hash

        - BlogItemModel (model for each blog item)
            int ID
            int UserID
            string PublishedName
            string Date
            string Title
            string Description
            string Image
            string Tags
            string Categories
            bool IsPublished
            bool IsDeleted (soft delete, still in database. can be recoverable )


            - DTOs (Data transfer model)/ folder
                - LoginDTO / file
                - CreateAccountDTO / file
                - PasswordDTO / file


-----------------------------------------------------------------------------------------------
Leo 
UserInterface {
	username: string;
	password: string;
	email: string;
	userId: number;
};

MediaInterface {
	title: string;
	type: string;
	platform: Array<string>;
	originalLanguage: string;
	translation: TransationInterface;
};

TranslationInterface {
	openRequests: [{
		requestName: string;
		requestDialogue: string;
		requestReferences: Array<string>; /* Images and video links */
		requestLanguage: Array<string>;
		submittedTranslations: [{
			translatorUserName: string;
			translatedDialogue: string;
			userScores: Array<number>;
		}];
		
	}];
};
-------------------------------------------------------------------
