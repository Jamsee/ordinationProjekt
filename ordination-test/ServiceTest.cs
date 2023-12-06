namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    [TestMethod]

    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
       
        var validPatientId = 1;
        var validLaegemiddelId = 100;
        var validDoser = new Dosis[2];
        var invalidStartDato = DateTime.Now.AddDays(-1); // Set to a past date
        var validSlutDato = DateTime.Now;

        // Act and Assert
        Assert.ThrowsException<ArgumentException>(() => service.OpretDagligSkaev(validPatientId, validLaegemiddelId, validDoser, invalidStartDato, validSlutDato));
        
        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
}