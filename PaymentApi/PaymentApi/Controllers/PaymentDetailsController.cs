using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PaymentApi.DBContext;

namespace PaymentApi.Controllers
{
    public class PaymentDetailsController : ApiController
    {
        private PaymentDetailDBEntities db = new PaymentDetailDBEntities();

        // GET: api/PaymentDetails
        public IQueryable<PaymentDetail> GetPaymentDetails()
        {
            return db.PaymentDetails;
        }

        // GET: api/PaymentDetails/5
        [ResponseType(typeof(PaymentDetail))]
        public async Task<IHttpActionResult> GetPaymentDetail(int id)
        {
            PaymentDetail paymentDetail = await db.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            return Ok(paymentDetail);
        }

        // PUT: api/PaymentDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentDetail.PMId)
            {
                return BadRequest();
            }

            db.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PaymentDetails
        [ResponseType(typeof(PaymentDetail))]
        public async Task<IHttpActionResult> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentDetails.Add(paymentDetail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = paymentDetail.PMId }, paymentDetail);
        }

        // DELETE: api/PaymentDetails/5
        [ResponseType(typeof(PaymentDetail))]
        public async Task<IHttpActionResult> DeletePaymentDetail(int id)
        {
            PaymentDetail paymentDetail = await db.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            db.PaymentDetails.Remove(paymentDetail);
            await db.SaveChangesAsync();

            return Ok(paymentDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentDetailExists(int id)
        {
            return db.PaymentDetails.Count(e => e.PMId == id) > 0;
        }
    }
}