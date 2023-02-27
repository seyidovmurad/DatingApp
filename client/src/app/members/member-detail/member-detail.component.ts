import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member | undefined;
  galleryOpt: NgxGalleryOptions[] = [];
  galleryImg: NgxGalleryImage[] = [];
  
  constructor(private router: ActivatedRoute, private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadMember();

    this.galleryOpt =  [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Fade,
        preview: false
      }
    ];
    
  }

  loadMember() {
    const username = this.router.snapshot.paramMap.get('username')?.toString() ?? '';
    
    this.memberService.getMember(username).subscribe({
      next: member => {
        this.member = member
        this.galleryImg = this.getImages();
        console.log(this.getImages());
        
      }
    });
  }

  private getImages() {
    if(!this.member) return [];

    return this.member.photos.map(photo => {
      return {
        small: photo.url,
        medium: photo.url,
        big: photo.url
      }
    });

    
    // const imgUrls = [];
    // for(let photo of this.member.photos) {
    //   imgUrls.push({
    //     small: photo.url,
    //     meduim: photo.url,
    //     big: photo.url
    //   });
    // }

    // return imgUrls
  }
}
