using System.Collections.Generic;
using BitbucketSharp.Models;
using RestSharp.Contrib;

namespace BitbucketSharp.Controllers
{
    /// <summary>
    /// Provides access to 'global' branches on a repository
    /// </summary>
    public class BranchesController : Controller
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        public RepositoryController Repository { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">A handle to the client</param>
        /// <param name="repo">The repository the branch belongs to</param>
        public BranchesController(Client client, RepositoryController repo) : base(client)
        {
            Repository = repo;
        }

        /// <summary>
        /// Access a specific branch via the slug
        /// </summary>
        /// <param name="branch">The repository branch</param>
        /// <returns></returns>
        public BranchController this[string branch]
        {
            get { return new BranchController(Client, this, branch); } 
        }

        /// <summary>
        /// Gets all the branches in this repository
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, BranchModel> GetBranches(bool forceCacheInvalidation = false)
        {
            return Client.Get<Dictionary<string, BranchModel>>(Uri, forceCacheInvalidation);
        }

        /// <summary>
        /// The URI of this controller
        /// </summary>
        public override string Uri
        {
            get { return Repository.Uri + "/branches"; }
        }
    }

    /// <summary>
    /// Access a specific branch
    /// </summary>
    public class BranchController : Controller
    {
        /// <summary>
        /// The branch name
        /// </summary>
        public string Name { get; private set; }

		/// <summary>
		/// Gets the name of the URL safe.
		/// </summary>
		/// <value>The name of the URL safe.</value>
		public string UrlSafeName
		{
			get
			{
				return HttpUtility.UrlEncode(Name);
			}
		}

        /// <summary>
        /// The repository this branch belongs to
        /// </summary>
        public BranchesController Branches { get; private set; }

        /// <summary>
        /// Gets the source this branch has
        /// </summary>
        public SourcesController Source
        {
            get { return new SourcesController(Client, this); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="branches"></param>
        /// <param name="branch"></param>
        public BranchController(Client client, BranchesController branches, string branch)
            : base(client)
        {
            Branches = branches;
            Name = branch;
        }

        /// <summary>
        /// The URI of this controller
        /// </summary>
        public override string Uri 
        {
            get { return Branches.Uri + "/" + UrlSafeName; }
        }
    }
}

