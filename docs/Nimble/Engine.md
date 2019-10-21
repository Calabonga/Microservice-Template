_Для версии ASP.NET Core 3.0._

В папке Engine как раз и находится Nimble Framework. Весь фреймворк состоит из трех основных понятий: `EntityManager`, `EntityValidator` и `ViewModelFactory`.

В этом разделе:

* EntityManager
    * EntityManagerContext
    * Principal
    * ViewModelFactory
    * Validator
    * Pipeline Create
        * OnCreateBeforeMappingAsync()
        * SetAuditInformation()
        * OnCreateBeforeAnyValidationsAsync()
        * ValidateUserAccessRights()
        * OnCreateBeforeSaveChangesAsync()
        * OnCreateAfterSaveChangesSuccessAsync()
        * OnCreateAfterSaveChangesFailedAsync()
    * Pipeline Update
        * SetAuditInformation()
        * OnUpdateBeforeMappingsAsync()
        * OnUpdateBeforeAnyValidationsAsync()
        * ValidateUserAccessRights()
        * OnUpdateBeforeSaveChangesAsync()
        * OnUpdateAfterSaveChangesFailedAsync()
        * OnUpdateAfterSaveChangesSuccessAsync()
* EnitytValidator
    * ValidateOnInsert()
    * ValidateOnUpdate()
    * ValidateOnInsertOrUpdate()
* ViewModelFactories
    * GenerateForCreateAsync()
    * GenerateForUpdateAsync()
Извините, страница в процессе наполнения.