import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SearchUser } from 'src/app/models/domain/profile/search-user';
import { Pagination } from 'src/app/models/helpers/pagination';
import { ProfilesRequest } from 'src/app/resolvers/requests/profiles-request';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.scss']
})
export class UserSearchComponent implements OnInit {

  searchForm: FormGroup;
  userProfiles: SearchUser[];
  pagination: Pagination;

  constructor(private formBuilder: FormBuilder, private profileService: ProfileService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.createSearchForm();
    this.subscribeData();
  }

  public searchUsers(onScroll = false) {
    const profilesRequest = new ProfilesRequest();
    profilesRequest.userName = this.searchForm.value?.userName;
    profilesRequest.pageNumber = onScroll ? this.pagination.currentPage : 1;
    this.profileService.getProfiles(profilesRequest).subscribe(res => {
      const users = res?.result.userProfiles;
      this.pagination = res.pagination;
      this.userProfiles = onScroll ? this.userProfiles.concat(users) : users;
    });
  }


  public onScroll() {
    if (this.userProfiles.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.searchUsers(true);
    }
  }

  public goToUserProfile(userId: string) {
    this.router.navigate(['profile/', userId]);
  }

  private createSearchForm() {
    this.searchForm = this.formBuilder.group({
      userName: ['']
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.userProfiles = data.searchResponse.result?.userProfiles;
      this.pagination = data.searchResponse.pagination;
    });
  }
}
